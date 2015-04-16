import sys
import os
import csv
import numpy


def read_file(filename):
	arr = numpy.load(filename)
	return arr


def transform_data_to_difference(real_filename, fake_filename):
	real = numpy.loadtxt(real_filename).T
	fake = numpy.loadtxt(fake_filename).T
	real_diff = []
	fake_diff = []
	for i in range(1, len(real)):
		real_diff.append( numpy.abs(real[i] - real[i-1]))
		fake_diff.append( numpy.abs(fake[i] - fake[i-1]))
		# fake_diff[i-1] = fake[i] - fake[i-1]

	return real_diff, fake_diff

def compute_discrete_prob(real_filename, fake_filename):
	bin_num = 500

	real, fake = transform_data_to_difference(real_filename, fake_filename)
	
	# print 'real', real
	real_bin = numpy.linspace(numpy.min(real),numpy.max(real),bin_num)
	# print 'real_bin', real_bin
	# digitize contains the idx of bin each value in data corresponds to
	real_digitize = numpy.digitize(real, real_bin)
	# print 'real_digitize', real_digitize

	
	# print fake
	fake_bin = numpy.linspace(numpy.min(fake),numpy.max(fake),bin_num)
	# print fake_bin
	# digitize contains the idx of bin each value in data corresponds to
	fake_digitize = numpy.digitize(fake, fake_bin)
	# print fake_digitize

	prob = numpy.zeros((bin_num,bin_num))
	real_prob = numpy.zeros(bin_num)
	fake_prob = numpy.zeros(bin_num)
	# print prob.shape

	for i in range(0, len(real)):
		x = real_digitize[i]-1
		y = fake_digitize[i]-1
		prob[x,y] = prob[x,y]  + 1
		real_prob[x] = real_prob[x] + 1
		fake_prob[y] = fake_prob[y] + 1

	prob = prob/len(real)
	real_prob = real_prob/len(real)
	fake_prob = fake_prob/len(real)
	'''
	print prob
	print real_prob
	print fake_prob
	'''
	return real, fake, prob, real_digitize, fake_digitize, real_prob, fake_prob
	
	
'''
compute mutual information of two time serials arrays
note the way of comuting is different from that in traditional, we follow the definition in CCS12 paper
'''
def compute_mutual_info(real_filename, fake_filename):	
	real, fake, prob, real_digitize, fake_digitize, real_prob, fake_prob = compute_discrete_prob(real_filename, fake_filename)
	sum = 0

	for i in range(0, len(real)):
		x = real_digitize[i]-1
		y = fake_digitize[i]-1

		# print 'prob[x,y]', prob[x,y]
		# print  'real_prob[x]', real_prob[x]
		# print 'fake_prob[y]', fake_prob[y]
		
		sum = sum + prob[x,y] * numpy.log(prob[x,y]/real_prob[x]/fake_prob[y])
	return sum





	

if __name__ == '__main__':

	algs = ['BE', 'NILL', 'LS1', 'LS2']

	for alg in algs:
		filedir = '..\\data\\simOutput\\'
		real_filename = filedir+alg+'_elecDemand.txt'
		fake_filename = filedir+alg+'_extEnergy.txt'
		sum = compute_mutual_info(real_filename, fake_filename)
		print alg, sum
	



