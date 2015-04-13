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


if __name__ == '__main__':
	house = "house_1"
	input_file = "..\\..\\smart-grid-workloads\\redd\\processData\\data\\"+house+"\\average_electricity.txt" 
	start_time = 15*12+8
	total_days = 3
	save_file = "..\\data\\3day_5min.txt"
	extract_data_for_exp(input_file, start_time, total_days, save_file)