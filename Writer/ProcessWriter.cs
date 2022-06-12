using System;

namespace SoarLib.Writer
{
    public class ProcessWriter
    {
        private ProcessWriterBase wb;
        private int _TraitCount;
        public int TraitCount
        {
            get { return _TraitCount; }
        }
        public ProcessWriter(ProcessModule module)
        {
            wb = new ProcessWriterBase(module);
        }
        public bool Write(string oldTrait, string NewTrait, TraitOption TraitOption)
        {
            wb.TraitOptions = TraitOption;
            _TraitCount = wb.WriteTrait(oldTrait, NewTrait);
            if (TraitCount != 0)
                return true;
            return false;
        }
        public bool Write(int Address, int buffer)
        {
            return wb.WriteInt(Address, buffer);
        }
        public bool Write(int Address, byte[] buffer)
        {
            return wb.WriteBytes(Address, buffer);
        }
        public bool Write(int Address, byte[] buffer, int Writelength)
        {
            return wb.WriteBytes(Address, buffer, Writelength);
        }
        public bool Write(int Address, float buffer)
        {
            return wb.WriteFloat(Address, buffer);
        }
        public IntPtr Create(int leng)
        {
            return wb.Create(leng);
        }
        public IntPtr Create(int leng, byte[] bytes)
        {
            return wb.Create(leng, bytes);
        }
        public bool Free(IntPtr Address)
        {
            return wb.Free(Address);
        }
    }
}
