using System.Collections.Generic;

namespace SoarLib.Reader
{
    public class ProcessReader
    {
        private ProcessReaderBase rb;
        private int _TraitCount;
        public int TraitCount
        {
            get { return _TraitCount; }
        }
        public ProcessReader(ProcessModule module)
        {
            rb = new ProcessReaderBase(module);
        }
        public List<int> Read(string Trait)
        {
            List<int> list = new List<int>();
            _TraitCount = rb.ReadTrait(Trait, list);
            return list;
        }

        public int Read(int address)
        {
            return rb.ReadInt(address);
        }
        public byte[] Read(int address, byte age = 0)
        {
            return rb.ReadBytes(address);
        }
        public byte[] Read(int address, int length)
        {
            return rb.ReadBytes(address, length);
        }
        public float Read(int address, float age = 0)
        {
            return rb.ReadFloat(address);
        }
        public int ReadModule(string moduleName, string ModuleOffset)
        {
            return rb.ReadModuleAddress(moduleName, ModuleOffset).ToInt32();
        }
        public int ReadModule(string moduleName,string ModuleOffset, string[] AddressOffset)
        {
            return rb.ReadModuleAddressToInt(moduleName, ModuleOffset, AddressOffset);
        }
    }
}
