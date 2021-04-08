import time
import struct

f = open(r'\\.\pipe\NPtest','r+b',0)
for i in range(0,10):
    st = 'Message[{0}]'.format(i).encode('ascii')

    f.write(struct.pack('I', len(st)) + st)
    f.seek(0)
    print('Wrote:',st)
    
    n = struct.unpack('I', f.read(4))[0]
    st = f.read(n).decode('ascii')
    f.seek(0)
    print('Read:', st)
    time.sleep(2)