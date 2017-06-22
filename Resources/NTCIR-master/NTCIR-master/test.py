nimport histsimilar as his
import os
import fnmatch
import sys

reload(sys)
sys.setdefaultencoding('utf8')

def getListFiles(path):
    ret = [] 
    for root, dirs, files in os.walk(path):  
        for filespath in fnmatch.filter(files,'*.jpg'): 
            ret.append(os.path.join(root,filespath)) 
    return ret

file1 = open('NTCIR/u1/filelist.txt','r')
file2 = open('NTCIR/u2/filelist.txt','r')
fo1 = open('NTCIR/u1/compare1.txt','w')
fo2 = open('NTCIR/u2/compare2.txt','w')
flog = open('NTCIR/log.txt','a+')
threshold = 0
num1 = 0
num2 = 0
'''
list1 = getListFiles('NTCIR/u1/2016')
#f1.write(list1[0]+'\n')
for index in range(len(list1)):
	num1 += 1
	file1.write(list1[index]+'\n')

list2 = getListFiles('NTCIR/u2/2016')
#f2.write(list2[0]+'\n')
for index in range(len(list2)):
	num2 += 1
	file2.write(list2[index]+'\n')

flog.write(str(num1)+'\n')
flog.write(str(num2)+'\n')
'''
if __name__ == '__main__':
	num1 = 0
	num2 = 0
	flog.write('*******\n')
	list1 = file1.readlines()
	for index in range(len(list1)-1):
		num1 += 1
		flog.write(str(num1)+'\n')
		try:
			value = his.calc_similar_by_path(list1[index].strip('\n'), list1[index+1].strip('\n'))
			print str(num1)+':'+ str(value)
			fo1.write(str(value) +'	'+ list1[index].strip('\n') +'	'+ list1[index+1].strip('\n') + '\n')
		except:
			print list1[index+1]

	flog.write('*******\n')
	list2 = file2.readlines()
	for index in range(len(list2)-1):
		num2 += 1
		flog.write(str(num2)+'\n')
		try:
			value = his.calc_similar_by_path(list2[index].strip('\n'), list2[index+1].strip('\n'))
			print str(num2)+':'+ str(value)
			fo2.write(str(value) +'	'+ list2[index].strip('\n') +'	'+ list2[index+1].strip('\n') + '\n')
		except:
			print list2[index+1]
