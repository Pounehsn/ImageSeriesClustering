file1 = open('NTCIR/u1/compare1.txt','r')
file2 = open('NTCIR/u2/compare2.txt','r')

num = 0
for line in file1.readlines():
    l = line.split('|')
    value = float(l[0])
    if value<0.3:
        num += 1
        #print sendpost(l[1],headers1)
        #print l[1]
        print num
