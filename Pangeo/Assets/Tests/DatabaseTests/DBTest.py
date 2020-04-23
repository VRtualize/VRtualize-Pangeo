import mysql.connector
import time

#Open up config file and get credentials
config_file = open("../../config")
configuration = config_file.readlines()
chost = configuration[1].split("\"")[3]
cuser = configuration[4].split("\"")[3]
cpasswd = configuration[5].split("\"")[3]
cdatabase = configuration[3].split("\"")[3]
cport = configuration[2].split("\"")[3]

print("Current configuration file properties:")
print("HOST: ",chost)
print("USER: ", cuser)
print("PASSWORD: ", cpasswd)
print("DATABASE: ", cdatabase)
print("PORT: ", cport)


#Connect to database
try:
    mydb = mysql.connector.connect(
      host=chost,
      user=cuser,
      passwd=cpasswd,
      database=cdatabase,
      port=cport,
      use_pure=True
      )
    mycursor = mydb.cursor(buffered=True)
except:
    print("Connection could not be made to the database.")
    print("Please make sure config file has proper credentials for connection.")
    print("In addition, make sure you have mysql-connector-python installed via pip.")

#TEST 1: Count Query
time1 = time.time()
mycursor.execute("SELECT COUNT(*) FROM image_cache")
time2 = time.time()
result =  mycursor.fetchall()
print("")
print("Current number of entries:")
print(result)
print("The count query took: ", str(time2-time1), " seconds")
print("COUNT QUERY SUCCESS")
print("")

#TEST 2: Column Names
time1 = time.time()
mycursor.execute("SHOW COLUMNS FROM image_cache",())
result =  mycursor.fetchall()
for i in result:
    print(i)
time2 = time.time()
print("The column name query took: ", str(time2-time1), " seconds")
correct_column_names = True
if(result[0] != ('quadkey', 'varchar(45)', 'NO', 'PRI', None, '')):
    correct_column_names = False
if(result[1] != ('zoomlevel', 'smallint', 'NO', '', None, '')):
    correct_column_names = False
if(result[2] != ('image_data', 'mediumblob', 'YES', '', None, '')):
    correct_column_names = False
if(result[3] != ('elevations', 'mediumblob', 'YES', '', None, '')):
    correct_column_names = False

if(correct_column_names):
    print("COLUMN PROPERTIES SUCCESS")
else:
    print("Column names or properties may be incorrect. Please check your schema.")
