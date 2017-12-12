from flask import Flask, request
import json
from flaskext.mysql import MySQL

app = Flask(__name__)
mysql = MySQL()
 
# MySQL configurations
app.config['MYSQL_DATABASE_USER'] = 'root'
app.config['MYSQL_DATABASE_PASSWORD'] = ''
app.config['MYSQL_DATABASE_DB'] = 'cities'
app.config['MYSQL_DATABASE_HOST'] = 'localhost'
mysql.init_app(app)

@app.route("/")
def hello():
    return "List of Cities Database"

@app.route('/countries')
def countries():
	with mysql.connect() as cursor:
		cursor.execute("SELECT id, name from countries")
		itList = [
			{
				"id" : row[0],
				"name" : row[1],
			}
			for row in cursor.fetchall() if row[1]
		]
		cursor.close()
		return json.dumps(itList)

@app.route('/regions')
def regions():
	country = request.args.get('country', '')
	with mysql.connect() as cursor:
		cursor.execute('SELECT id, name from regions WHERE country_id=%s', (country,))
		itList = [
			{
				"id" : row[0],
				"name" : row[1],
			}
			for row in cursor.fetchall() if row[1]
		]
		cursor.close()
		return json.dumps(itList)

@app.route('/cities')
def cities():
	country = request.args.get('country', '')
	region = request.args.get('region', '')
	with mysql.connect() as cursor:
		cursor.execute('SELECT cities.id, cities.name, regions.name, countries.name from cities '
		'INNER JOIN regions ON cities.region_id = regions.id '
		'INNER JOIN countries ON cities.country_id = countries.id '
		'WHERE cities.country_id=%s AND cities.region_id=%s', (country,region))
		itList = [
			{
				"id" : row[0],
				"name" : row[1],
				"region" : row[2],
				"country" : row[3]
			}
			for row in cursor.fetchall() if row[1]
		]
		cursor.close()
		return json.dumps(itList)

@app.route('/city')
def ciy():
	city = request.args.get('id', '')
	with mysql.connect() as cursor:
		cursor.execute('SELECT cities.id, cities.name, regions.name, countries.name from cities '
		'INNER JOIN regions ON cities.region_id = regions.id '
		'INNER JOIN countries ON cities.country_id = countries.id '
		'WHERE cities.id=%s', (city,))
		data = cursor.fetchone()
		item = {
			"id" : data[0],
			"name" : data[1],
			"region" : row[2],
			"country" : row[3]
		}
		cursor.close()
		return json.dumps(item)
