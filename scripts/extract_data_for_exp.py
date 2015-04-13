import sys
import os
import csv
import numpy


def extract_data_for_exp(input_file, start_time, total_days, save_file):
	with open(input_file) as f:
		lines = f.readlines()[start_time: start_time+total_days*24*12]
		
	data = []
	for line in lines:
		item = line.split()[0]
		data.append(float(item))
	print len(data)

	numpy.savetxt(save_file, data,'%0.5f')

def extract_timeStamp_for_exp(input_file, start_time, total_days, save_file):
	with open(input_file) as f:
		lines = f.readlines()[start_time: start_time+total_days*24*12]
		
	data = []
	for line in lines:
		item = line.split()[0]
		data.append(int(item))
	print len(data)

	numpy.savetxt(save_file, data, fmt = '%d')


if __name__ == '__main__':
	house = "house_1"
	input_file = "..\\..\\smart-grid-workloads\\redd\\processData\\data\\"+house+"\\average_electricity.txt" 
	time_input_file = "..\\..\\smart-grid-workloads\\redd\\processData\\data\\"+house+"\\timestamp.txt" 
	start_time = 15*12+8
	total_days = 3
	save_file = "..\\data\\3day_5min.txt"
	time_save_file = "..\\data\\3day_5min_timestamp.txt"
	extract_data_for_exp(input_file, start_time, total_days, save_file)
	extract_timeStamp_for_exp(time_input_file, start_time, total_days, time_save_file)