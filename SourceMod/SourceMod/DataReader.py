import time
import struct
import re
import win32pipe
import win32file
import win32event
import pywintypes
import sys
import threading

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
    "ground_pollution",
    "residential_demand",
    "commercial_demand",
    "workplace_demand",
    "actual_residential_demand",
    "actual_commercial_demand",
    "actual_workplace_demand"
]
pms = [0] * len(pmids)
pmd = {}

outqueue = [];

def updatepms(st):
    l = st.split(", ")
    print(st)
    print(len(pms))
    print(len(l))
    for i in range (len(pms)):
        pms[i] = l[i]
        pmd[pmids[i]] = pms[i]
    print(pmd)

def iothread(loc):
    pipealias1 = r'\\.\pipe\NP2'

    bufsize = 1024*64

    notexiting = True
    while(notexiting):
        try:
            pipe = win32pipe.CreateNamedPipe(pipealias1,
                win32pipe.PIPE_ACCESS_DUPLEX,
                win32pipe.PIPE_TYPE_BYTE|win32pipe.PIPE_READMODE_BYTE|win32pipe.PIPE_WAIT, 
                1, bufsize, bufsize, 1, None) 
            try:
                print("waiting for connection on pipealias1")
                win32pipe.ConnectNamedPipe(pipe,None)
                print("connection established")
                while True:
                    inl = win32file.ReadFile(pipe, 4) #read the length of C# message
                    print(inl)
                    leng = int.from_bytes(inl[1], sys.byteorder) 
                    print(leng)
                    ins = win32file.ReadFile(pipe,leng) #read the C# message
                    print('Read:',ins[1].decode())
                    updatepms(ins[1].decode())
                    st = "error"
                    loc.acquire()
                    if len(outqueue) == 0:
                        st = "noaction"
                    else:
                        st = outqueue[0]
                        outqueue.remove(outqueue[0])
                    loc.release()
                    send = struct.pack('I', len(st)) + st.encode()
                    win32file.WriteFile(pipe, send)
                    print('wrote: ', send)
                    inw = win32file.ReadFile(pipe, 4) #the pipe will read its own messages
                    inw2 = win32file.ReadFile(pipe, len(st)) # the pipe will read its own messages
                    time.sleep(1)
            finally:
                win32file.CloseHandle(pipe)
        except Exception as ex:
            print("error with pipalias1: ",ex)

def action_decider(loc):
    notexiting = True
    while(notexiting):
        st = input()
        loc.acquire
        outqueue.append(st)
        loc.release

#main
lock = threading.Lock()
t1 = threading.Thread(target=iothread, args=(lock,))
t2 = threading.Thread(target=action_decider, args=(lock,))
t1.start()
t2.start()
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
#residential_demand
#commercial_demand
#workplace_demand
#actual_residential_demand
#actual_commercial_demand
#actual_workplace_demand