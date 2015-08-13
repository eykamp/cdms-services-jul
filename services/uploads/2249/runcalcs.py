import re
from subprocess import call
import pypyodbc
import sys


first_run = False
# print_ids_and_exit = True

#where = 'localhost'        
server = 'kettle'              # 'devrig05', 'kettle'
url_prefix = 'qa'         # 'localhost', 'qa'


# Either this:
# watershed = 'Methow'    # Must be single value for the moment
# iteration = 2013        # Can be None for all iterations

# or this:
programsiteid_list = ['7981', '132', '51115', '66391']    # strings only!



# ['RBT', 'Aux', 'AuxAndRBT', 'Orthogonal', 'SiteGDB', 'StreamTemp', 'HydroModel', 'HydroModelDataPrep']
# 'all', 'none', or a list: ['RBT', 'Aux', 'AuxAndRBT']; order not important
active_engines =  ['Orthogonal', 'HydroModel', 'Aux', 'AuxAndRBT', 'RBT', 'HydroModelDataPrep']  
initial_engine_list = ['Orthogonal']        # These are the first metric run we'll start with

########################################################
# Stuff that shouldn't need to be monkeyed with
database = 'champ'


# first_run = False
# if len(sys.argv) > 1:
#     if sys.argv[1] == 'init':
#         first_run = True


# Other global stuff
print("Connecting... ", end="", flush=True)
connection = pypyodbc.connect('DRIVER={SQL Server};Server=' + server + ';Database=' + database + ';Trusted_Connection=True;')
print("Ok.")




def run_sql(sql):
    connection.cursor().execute(sql).commit()

def disable_all_engine_types():
    run_sql("update [MetricEngineType] set engineisavailable = 0")


def make_in_clause(itemlist):
    in_clause = ""

    for item in itemlist:
        if in_clause != "":
            in_clause += ","

        in_clause += "'" + item + "'"

    return in_clause


''' Call with list of engines or 'all' to enable everything, or 'none' to disable all engines '''
def set_engines_available(engine_list):
    if engine_list == 'all':
        run_sql("update MetricEngineType set engineisavailable = 1")
        return

    disable_all_engine_types()

    if engine_list == 'none':
        return

    run_sql("update MetricEngineType set engineisavailable = 1 where name in (" + make_in_clause(engine_list) + ")")


def get_vals(sql):
    cur = connection.cursor()
    cur.execute(sql)
    
    vals = []

    for row in cur.fetchall():
        vals.append(str(row[0]))

    return vals


def get_program_site_ids_for_watershed(watershed, iteration=None):
    sql = "select distinct programsiteid from vProgramSiteVisitLite where watershedname = '" + watershed + "'"
    if iteration is not None:
        sql += " and iterationName = '" + str(iteration) + "'"
    return get_vals(sql)


def get_visit_ids_for_watershed(watershed, iteration=None):
    sql = "select distinct visitid from vProgramSiteVisitLite where watershedname = '" + watershed + "'"
    if iteration is not None:
        sql += " and iterationName = '" + str(iteration) + "'"

    return get_vals(sql)

def get_site_ids_for_programsite_ids(programsiteid_list):
    # sql = "select distinct siteid from vProgramSiteVisitLite where programsiteid in( " + ",".join(programsiteid_list) + ")"
    return programsiteid_list    


def get_visit_ids_for_programsite_ids(programsiteid_list):
    sql = "select distinct visitid from vProgramSiteVisitLite where programsiteid in( " + ",".join(programsiteid_list) + ")"
    return get_vals(sql)    


def clear_engine_status_records():
    run_sql(""" update [ProgramSiteVisitMetricEngineCalcStatus] 
                set NeedToRequestMetricCalc = 0, statusID = 1, needtoreceivemetricresults = 0 """)


def set_need_to_request(engine_list):
    sql = """   update [ProgramSiteVisitMetricEngineCalcStatus] set NeedToRequestMetricCalc = 1, calcEngineHandleId = NULL 
                where engineId in (""" + ','.join(get_engine_id_list_from_engines(engine_list)) + """ ) 
                """
    run_sql(sql)


def get_engine_id_list_from_engines(engine_list):
    sql = 'select engineid from MetricEngineType where name in (' + make_in_clause(engine_list) + ')'
    return get_vals(sql)
    

# print(get_program_site_ids_for_watershed(watershed, iteration))

def main():

    print("================================================================")

    try:
        watershed

    except NameError:
        visit_ids = get_visit_ids_for_programsite_ids(programsiteid_list)
        site_ids = get_site_ids_for_programsite_ids(programsiteid_list)

    else:
        visit_ids = get_visit_ids_for_watershed(watershed, iteration)
        site_ids = get_program_site_ids_for_watershed(watershed, iteration)


    try:
        print_ids_and_exit
        if print_ids_and_exit:
            print("Visits:", visit_ids)
            print("Sites:", site_ids)
            quit()
    except NameError:
        pass

    if first_run:
        set_engines_available(active_engines)
        clear_engine_status_records()
        set_need_to_request(initial_engine_list)


    site_urls = ['https://%%WHERE%%.champmonitoring.org/Batch/ProcessMetricsForProgramSite/%%SITE%%']

    for site in site_ids:
        for site_step in site_urls:
            cmd = re.sub('%%SITE%%', str(site), site_step)
            cmd = re.sub('%%WHERE%%', url_prefix, cmd)

            print("\n\nURL:\n", cmd)
            call(['c:/Program Files/Utils/curl.exe', '-k', cmd])


    visit_urls = [
            'https://%%WHERE%%.champmonitoring.org/Batch/comparefilesystemforvisit/%%VISIT%%/false',
            'https://%%WHERE%%.champmonitoring.org/Batch/ApplyfilesystemchangesForVisit/%%VISIT%%',
            'https://%%WHERE%%.champmonitoring.org/Batch/persistmeasurementsforvisit/%%VISIT%%'
    ]

    for visit in visit_ids:
        for visit_step in visit_urls:
            cmd = re.sub('%%VISIT%%', str(visit), visit_step)
            cmd = re.sub('%%WHERE%%', url_prefix, cmd)

            print("\n\nURL:\n", cmd)
            call(['c:/Program Files/Utils/curl.exe', '--insecure', cmd])



main()

