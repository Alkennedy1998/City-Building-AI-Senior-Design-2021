import time
import struct
import re

pmids = [
    "population", 
    "happiness",
    "life_span",
    "sheltered",
    "sick_count",
    "electricity_consumption",
    "water_consumption",
    "garbage",
    "unemployment",
    "criminal_amount",
    "extra_criminals",
    "education_1_capacity",
    "education_1_need",
    "education_1_rate",
    "education_2_capacity",
    "education_2_need",
    "education_2_rate",
    "education_3_capacity",
    "education_3_need",
    "education_3_rate",
    "water_pollution",
    "ground_pollution"
]
pms = [0] * len(pmids)
pmd = {}

def dosomething(st):
    x = re.findall("Pop: ", st)
    if len(x) > 0:
        i = re.findall(r'\d+', st)
        print("found population variable: ", i[0])
    l = st.split(", ");
    for i in range (len(pms)):
        pms[i] = l[i]
        pmd[pmids[i]] = pms[i]
    print(pmd)

    

f = open(r'\\.\pipe\NP','r+b',0)
while True:
    st = 'echo'.encode('ascii')

    f.write(struct.pack('I', len(st)) + st)
    f.seek(0)
    print('Wrote:',st)
    n = struct.unpack('I', f.read(4))[0]
    st = f.read(n).decode('ascii')
    f.seek(0)
    print('Read:', st)
    if st != "echo":
        dosomething(st)
    time.sleep(1)
f.close()



#population
#happiness
#life_span
#sheltered
#sick_count
#electricity_consumption
#water_consumption
#garbage
#unemployment
#criminal_amount
#extra_criminals
#education_1_capacity
#education_1_need
#education_1_rate
#education_2_capacity
#education_2_need
#education_2_rate
#education_3_capacity
#education_3_need
#education_3_rate
#water_pollution
#ground_pollution