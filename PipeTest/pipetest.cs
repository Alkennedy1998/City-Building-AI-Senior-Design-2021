public static void Main(String[] args)
{
    go();
}

void go()
{
    var server = new NamedPipServerStream("NPtest");
    Console.WriteLine("Waiting for connection..");
    server.WaitForConnection();

    Console.WriteLine("Conected.");
    var br = new BinaryReader(server);
    var bw = new BinaryWriter(server);
    while(true)
    {
        try
        {
            var le = (int) br.ReadUInt32();
            var st = new string(br.ReadChars(len));

            Console.WriteLine("read: \"{0}\"",st);
            st = new string(st.Reverse().ToArray());

            var buf = Encoding.ASCII.GetBytes(st);
            bw.Write((uint) buf.Length);
            bw.Write(buf);
            Console.WriteLine("Wrote: \"{0}\"", st);
        }
        catch (EndOfStreamException)
        {
            break;
        }
    }
    Console.WriteLine("Client disconnected");
    server.Close();
    server.Dispose();
}