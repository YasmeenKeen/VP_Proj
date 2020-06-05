import lxml.etree as ET
import os
f = open("Output.txt", "r")
file = f.read()
root = ET.Element("html")
body = ET.SubElement(root, "body")
elemDict ={}
elemDict['b']=body
for line in file.split('\n'):
    if(line!=''):
        arr = line.split(' ')
        textVal = ''
        attrs= {}
        parent = 'b'
        for i in range(2,len(arr)):
            attrName = arr[i].split('=')[0]
            attrVal = arr[i].split('=')[1]
            if(attrName =='text'):
                textVal = attrVal
            elif(attrName=='childof'):
                parent = attrVal
            elif(attrName=='color'):
                if('style' in attrs.keys()):
                    attrs['style'] = attrs['style']+';'+'color:'+attrVal
                else:
                    attrs['style']='color:'+attrVal
            elif(attrName=='style'):
                if('style' in attrs.keys()):
                    attrs['style']= attrs['style'] +';'+attrVal
                else:
                    attrs['style'] = attrVal

            else:
                attrs[attrName] = attrVal
        elemDict[arr[0]] = ET.SubElement(elemDict[parent],arr[1],attrib=attrs)
        if(textVal!=''):
            elemDict[arr[0]].text = textVal
tree = ET.ElementTree(root)
outputPath = 'test.html'
tree.write(outputPath,pretty_print=True, encoding='utf-8')
os.system('test.html')
