import lxml.etree as ET
import os
f = open("Output.txt", "r")
file = f.read()
root = ET.Element("html")
body = ET.SubElement(root, "body")
elemDict ={}
for line in file.split('\n'):
    if(line!=''):
        arr = line.split(' ')
        textVal = ''
        attrs= {}
        for i in range(2,len(arr)):
            attrName = arr[i].split('=')[0]
            attrVal = arr[i].split('=')[1]
            if(attrName =='text'):
                textVal = attrVal
            else:
                attrs[attrName] = attrVal
        elemDict[arr[0]] = ET.SubElement(body,arr[1],attrib=attrs)
        if(textVal!=''):
            elemDict[arr[0]].text = textVal
tree = ET.ElementTree(root)
outputPath = 'test.html'
tree.write(outputPath,pretty_print=True, encoding='utf-8')
os.system('test.html')
