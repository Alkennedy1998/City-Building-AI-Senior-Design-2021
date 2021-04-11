import time
import struct
import re

def dosomething(st):
    x = re.findall("Pop: ", st)
    if len(x) > 0:
        i = re.findall(r'\d+', st)
        print("found population variable: ", i[0])

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
