########### Python 2.7 #############
import httplib, urllib, base64
import json

key1 = '5417b3e62d4a4c67a5a3f5a3feadb5d9'
key2 = 'feaf4fc6b0c6421fbdb1c44acf166901'

headers1 = {
    # Request headers
    'Content-Type': 'application/octet-stream',
    'Ocp-Apim-Subscription-Key': key1,
}

headers2 = {
    # Request headers
    'Content-Type': 'application/octet-stream',
    'Ocp-Apim-Subscription-Key': key2,
}

params = urllib.urlencode({
    # Request parameters
    'visualFeatures': 'Categories,Tags,Color,Description',
    'details': '',
    'language': 'en',
})

file2 = open('NTCIR/u2/compare2.txt','r')

fo2 = open('NTCIR/u2/vision2.txt','a')

def sendpost(path,headers):
    body = open(path, 'rb').read();
    try:
        conn = httplib.HTTPSConnection('westus.api.cognitive.microsoft.com')
        conn.request("POST", "/vision/v1.0/analyze?%s" % params, body=body, headers=headers)
        response = conn.getresponse()
        data = response.read()
        result = json.loads(data)
        conn.close()
        return data
    except Exception as e:
        return 'error'

num = 0
for line in file2.readlines():
    l = line.split('|')
    value = float(l[0])
    if value<0.52:
        num += 1
        if num<=2858:
            continue
        line = str(num)+'|'+ l[1]+'|'+sendpost(l[1],headers2)
        fo2.write(line+'\n')
        print num

print num
####################################








