using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static SoarLib.ASM.AsmBasic;

namespace SoarLib.ASM
{
    public class Asm
    {
        private byte[] _asm;
        public void SetAsm(byte[] buf_asm)
        {
            _asm = buf_asm;
        }
        public byte[] GetAsm()
        {
            return _asm;
        }
        private byte[] Add(byte[] a, byte[] b)
        {
            ArrayList retArray = new ArrayList();
            retArray.AddRange(a);
            retArray.AddRange(b);
            _asm = (byte[])retArray.ToArray(typeof(byte));
            return _asm;
        }

        public void leave()
        {
            Add(_asm, Analysis("C9"));
        }
        public void pushad()
        {
            Add(_asm, Analysis("60"));
        }
        public void popad()
        {
            Add(_asm, Analysis("61"));
        }
        public void nop()
        {
            Add(_asm, Analysis("90"));
        }
        public void ret()
        {
            Add(_asm, Analysis("C3"));
        }
        public void in_al_dx()
        {
            Add(_asm, Analysis("EC"));
        }
        public void test_eax_eax()
        {
            Add(_asm, Analysis("85C0"));
        }
        public void add_eax_edx()
        {
            Add(_asm, Analysis("03C2"));
        }
        public void add_ebx_eax()
        {
            Add(_asm, Analysis("03D8"));
        }

        public void add_eax_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("0305")), BitConverter.GetBytes(a));
        }
        public void add_ebx_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("031D")), BitConverter.GetBytes(a));
        }
        public void add_ebp_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("032D")), BitConverter.GetBytes(a));
        }
        public void add_eax(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("83C0")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("05")), BitConverter.GetBytes(a));
        }
        public void add_ebx(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("83C3")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("81C3")), BitConverter.GetBytes(a));
        }
        public void add_ecx(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("83C1")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("81C1")), BitConverter.GetBytes(a));
        }
        public void add_edx(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("83C2")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("81C2")), BitConverter.GetBytes(a));
        }
        public void add_esi(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("83C6")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("81C6")), BitConverter.GetBytes(a));
        }
        public void add_esp(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("83C4")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("81C4")), BitConverter.GetBytes(a));
        }

        public void call_eax()
        {
            Add(_asm, Analysis("FFD0"));
        }
        public void call_ebx()
        {
            Add(_asm, Analysis("FFD3"));
        }
        public void call_ecx()
        {
            Add(_asm, Analysis("FFD1"));
        }
        public void call_edx()
        {
            Add(_asm, Analysis("FFD2"));
        }
        public void call_esi()
        {
            Add(_asm, Analysis("FFD6"));
        }
        public void call_esp()
        {
            Add(_asm, Analysis("FFD4"));
        }
        public void call_ebp()
        {
            Add(_asm, Analysis("FFD5"));
        }
        public void call_edi()
        {
            Add(_asm, Analysis("FFD7"));
        }
        public void call_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("FF15")), BitConverter.GetBytes(a));
        }
        public void call_dword_ptr_eax()
        {
            Add(_asm, Analysis("FF10"));
        }
        public void call_dword_ptr_ebx()
        {
            Add(_asm, Analysis("FF13"));
        }
        public void cmp_eax(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("83F8")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("3D")), BitConverter.GetBytes(a));
        }
        public void cmp_eax_edx()
        {
            Add(_asm, Analysis("3BC2"));
        }
        public void cmp_eax_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("3B05")), BitConverter.GetBytes(a));
        }
        public void cmp_dword_ptr_eax(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("3905")), BitConverter.GetBytes(a));
        }
        public void dec_eax()
        {
            Add(_asm, Analysis("48"));
        }
        public void dec_ebx()
        {
            Add(_asm, Analysis("4B"));
        }
        public void dec_ecx()
        {
            Add(_asm, Analysis("49"));
        }
        public void dec_edx()
        {
            Add(_asm, Analysis("4A"));
        }
        public void idiv_eax()
        {
            Add(_asm, Analysis("F7F8"));
        }
        public void idiv_ebx()
        {
            Add(_asm, Analysis("F7FB"));
        }
        public void idiv_ecx()
        {
            Add(_asm, Analysis("F7F9"));
        }
        public void idiv_edx()
        {
            Add(_asm, Analysis("F7FA"));
        }
        public void imul_eax_edx()
        {
            Add(_asm, Analysis("0FAFC2"));
        }
        public void imul_eax(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("6BC0")), BitConverter.GetBytes(a));
        }
        public void imulb_eax(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("69C0")), BitConverter.GetBytes(a));
        }
        public void inc_eax()
        {
            Add(_asm, Analysis("40"));
        }
        public void inc_ebx()
        {
            Add(_asm, Analysis("43"));
        }
        public void inc_ecx()
        {
            Add(_asm, Analysis("41"));
        }
        public void inc_edx()
        {
            Add(_asm, Analysis("42"));
        }
        public void inc_edi()
        {
            Add(_asm, Analysis("47"));
        }
        public void inc_esi()
        {
            Add(_asm, Analysis("46"));
        }
        public void inc_dword_ptr_eax()
        {
            Add(_asm, Analysis("FF00"));
        }
        public void inc_dword_ptr_ebx()
        {
            Add(_asm, Analysis("FF03"));
        }
        public void inc_dword_ptr_ecx()
        {
            Add(_asm, Analysis("FF01"));
        }
        public void inc_dword_ptr_edx()
        {
            Add(_asm, Analysis("FF02"));
        }
        public void jmp_eax()
        {
            Add(_asm, Analysis("FFE0"));
        }
        public void mov_dword_ptr_eax(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("A3")), BitConverter.GetBytes(a));
        }
        public void mov_eax(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("B8")), BitConverter.GetBytes(a));
        }
        public void mov_ebx(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("BB")), BitConverter.GetBytes(a));
        }
        public void mov_ecx(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("B9")), BitConverter.GetBytes(a));
        }
        public void mov_edx(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("BA")), BitConverter.GetBytes(a));
        }
        public void mov_esi(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("BE")), BitConverter.GetBytes(a));
        }
        public void mov_esp(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("BC")), BitConverter.GetBytes(a));
        }
        public void mov_ebp(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("BD")), BitConverter.GetBytes(a));
        }
        public void mov_edi(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("BF")), BitConverter.GetBytes(a));
        }
        public void mov_ebx_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("8B1D")), BitConverter.GetBytes(a));
        }
        public void mov_ecx_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("8B0D")), BitConverter.GetBytes(a));
        }
        public void mov_edx_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("8B15")), BitConverter.GetBytes(a));
        }
        public void mov_esi_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("8B35")), BitConverter.GetBytes(a));
        }
        public void mov_esp_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("8B25")), BitConverter.GetBytes(a));
        }
        public void mov_eax_dword_ptr_eax()
        {
            Add(_asm, Analysis("8B00"));
        }
        public void mov_eax_dword_ptr_ebp()
        {
            Add(_asm, Analysis("8B4500"));
        }
        public void mov_eax_dword_ptr_ebx()
        {
            Add(_asm, Analysis("8B03"));
        }
        public void mov_eax_dword_ptr_ecx()
        {
            Add(_asm, Analysis("8B01"));
        }
        public void mov_eax_dword_ptr_edx()
        {
            Add(_asm, Analysis("8B02"));
        }
        public void mov_eax_dword_ptr_edi()
        {
            Add(_asm, Analysis("8B07"));
        }
        public void mov_eax_dword_ptr_esp()
        {
            Add(_asm, Analysis("8B0424"));
        }
        public void mov_eax_dword_ptr_esi()
        {
            Add(_asm, Analysis("8B06"));
        }
        public void mov_eax_dword_ptr_eax_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B40")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B80")), BitConverter.GetBytes(a));
        }
        public void mov_eax_dword_ptr_ebx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B43")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B83")), BitConverter.GetBytes(a));
        }
        public void mov_eax_dword_ptr_ecx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B41")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B81")), BitConverter.GetBytes(a));
        }
        public void mov_eax_dword_ptr_edx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B42")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B82")), BitConverter.GetBytes(a));
        }
        public void mov_eax_dword_ptr_edi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B47")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B87")), BitConverter.GetBytes(a));
        }
        public void mov_eax_dword_ptr_ebp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B45")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B85")), BitConverter.GetBytes(a));
        }
        public void mov_eax_dword_ptr_esi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B46")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B86")), BitConverter.GetBytes(a));
        }
        public void mov_ebx_dword_ptr_eax_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B58")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B98")), BitConverter.GetBytes(a));
        }
        public void mov_ebx_dword_ptr_ebx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B5B")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B9B")), BitConverter.GetBytes(a));
        }
        public void mov_ebx_dword_ptr_ecx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B59")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B99")), BitConverter.GetBytes(a));
        }
        public void mov_ebx_dword_ptr_edx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B5A")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B9A")), BitConverter.GetBytes(a));
        }
        public void mov_ebx_dword_ptr_edi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B5F")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B9F")), BitConverter.GetBytes(a));
        }
        public void mov_ebx_dword_ptr_ebp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B5D")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B9D")), BitConverter.GetBytes(a));
        }
        public void mov_ebx_dword_ptr_esi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B5E")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B9E")), BitConverter.GetBytes(a));
        }
        public void mov_ecx_dword_ptr_eax_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B48")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B88")), BitConverter.GetBytes(a));
        }
        public void mov_ecx_dword_ptr_ebx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B4B")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B8B")), BitConverter.GetBytes(a));
        }
        public void mov_ecx_dword_ptr_ecx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B49")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B89")), BitConverter.GetBytes(a));
        }
        public void mov_ecx_dword_ptr_edx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B4A")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B8A")), BitConverter.GetBytes(a));
        }
        public void mov_ecx_dword_ptr_edi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B4F")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B8F")), BitConverter.GetBytes(a));
        }
        public void mov_ecx_dword_ptr_ebp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B4D")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B8D")), BitConverter.GetBytes(a));
        }
        public void mov_ecx_dword_ptr_esi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B4E")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B8E")), BitConverter.GetBytes(a));
        }
        public void mov_edx_dword_ptr_eax_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B50")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B90")), BitConverter.GetBytes(a));
        }
        public void mov_edx_dword_ptr_ebx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B53")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B93")), BitConverter.GetBytes(a));
        }
        public void mov_edx_dword_ptr_ecx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B51")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B91")), BitConverter.GetBytes(a));
        }
        public void mov_edx_dword_ptr_edx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B52")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B92")), BitConverter.GetBytes(a));
        }
        public void mov_edx_dword_ptr_edi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B57")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B97")), BitConverter.GetBytes(a));
        }
        public void mov_edx_dword_ptr_ebp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B55")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B95")), BitConverter.GetBytes(a));
        }
        public void mov_edx_dword_ptr_esi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B56")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B96")), BitConverter.GetBytes(a));
        }

        public void mov_ebx_dword_ptr_eax()
        {
            Add(_asm, Analysis("8B18"));
        }

        public void mov_ebx_dword_ptr_ebp()
        {
            Add(_asm, Analysis("8B5D00"));
        }

        public void mov_ebx_dword_ptr_ebx()
        {
            Add(_asm, Analysis("8B1B"));
        }


        public void mov_ebx_dword_ptr_ecx()
        {
            Add(_asm, Analysis("8B19"));
        }

        public void mov_ebx_dword_ptr_edx()
        {
            Add(_asm, Analysis("8B1A"));
        }

        public void mov_ebx_dword_ptr_edi()
        {
            Add(_asm, Analysis("8B1F"));
        }

        public void mov_ebx_dword_ptr_esi()
        {
            Add(_asm, Analysis("8B1E"));
        }

        public void mov_ecx_dword_ptr_eax()
        {
            Add(_asm, Analysis("8B08"));
        }

        public void mov_ecx_dword_ptr_ebp()
        {
            Add(_asm, Analysis("8B4D00"));
        }

        public void mov_ecx_dword_ptr_ebx()
        {
            Add(_asm, Analysis("8B0B"));
        }

        public void mov_ecx_dword_ptr_ecx()
        {
            Add(_asm, Analysis("8B09"));
        }

        public void mov_ecx_dword_ptr_edx()
        {
            Add(_asm, Analysis("8B0A"));
        }

        public void mov_ecx_dword_ptr_edi()
        {
            Add(_asm, Analysis("8B0F"));
        }

        public void mov_ecx_dword_ptr_esi()
        {
            Add(_asm, Analysis("8B0E"));
        }

        public void mov_edx_dword_ptr_eax()
        {
            Add(_asm, Analysis("8B10"));
        }

        public void mov_edx_dword_ptr_ebp()
        {
            Add(_asm, Analysis("8B5500"));
        }

        public void mov_edx_dword_ptr_ebx()
        {
            Add(_asm, Analysis("8B13"));
        }

        public void mov_edx_dword_ptr_ecx()
        {
            Add(_asm, Analysis("8B11"));
        }
        public void mov_edx_dword_ptr_edx()
        {
            Add(_asm, Analysis("8B12"));
        }
        public void mov_edx_dword_ptr_edi()
        {
            Add(_asm, Analysis("8B17"));
        }
        public void mov_edx_dword_ptr_esi()
        {
            Add(_asm, Analysis("8B16"));
        }
        public void mov_edx_dword_ptr_esp()
        {
            Add(_asm, Analysis("8B1424"));
        }
        public void mov_eax_ebp()
        {
            Add(_asm, Analysis("8BC5"));
        }
        public void mov_eax_ebx()
        {
            Add(_asm, Analysis("8BC3"));
        }
        public void mov_eax_ecx()
        {
            Add(_asm, Analysis("8BC1"));
        }
        public void mov_eax_edi()
        {
            Add(_asm, Analysis("8BC7"));
        }
        public void mov_eax_edx()
        {
            Add(_asm, Analysis("8BC2"));
        }
        public void mov_eax_esi()
        {
            Add(_asm, Analysis("8BC6"));
        }
        public void mov_eax_esp()
        {
            Add(_asm, Analysis("8BC4"));
        }
        public void mov_ebx_eax()
        {
            Add(_asm, Analysis("8BD8"));
        }
        public void mov_ebx_ecx()
        {
            Add(_asm, Analysis("8BD9"));
        }
        public void mov_ebx_edi()
        {
            Add(_asm, Analysis("8BDF"));
        }
        public void mov_ebx_edx()
        {
            Add(_asm, Analysis("8BDA"));
        }
        public void mov_ebx_esi()
        {
            Add(_asm, Analysis("8BDE"));
        }
        public void mov_ebx_esp()
        {
            Add(_asm, Analysis("8BDC"));
        }
        public void mov_ecx_eax()
        {
            Add(_asm, Analysis("8BC8"));
        }
        public void mov_ecx_ebx()
        {
            Add(_asm, Analysis("8BCB"));
        }
        public void mov_ecx_edi()
        {
            Add(_asm, Analysis("8BCF"));
        }
        public void mov_ecx_edxi()
        {
            Add(_asm, Analysis("8BCA"));
        }
        public void mov_ecx_esi()
        {
            Add(_asm, Analysis("8BCE"));
        }
        public void mov_ecx_esp()
        {
            Add(_asm, Analysis("8BCC"));
        }
        public void mov_edx_ebp()
        {
            Add(_asm, Analysis("8BD5"));
        }
        public void mov_edx_ebx()
        {
            Add(_asm, Analysis("8BD3"));
        }
        public void mov_edx_ecx()
        {
            Add(_asm, Analysis("8BD1"));
        }
        public void mov_edx_edi()
        {
            Add(_asm, Analysis("8BD7"));
        }
        public void mov_edx_esi()
        {
            Add(_asm, Analysis("8BD6"));
        }
        public void mov_edx_esp()
        {
            Add(_asm, Analysis("8BD4"));
        }
        public void mov_esi_ebx()
        {
            Add(_asm, Analysis("8BF3"));
        }
        public void mov_esi_ecx()
        {
            Add(_asm, Analysis("8BF1"));
        }
        public void mov_esi_edi()
        {
            Add(_asm, Analysis("8BF7"));
        }
        public void mov_esi_esp()
        {
            Add(_asm, Analysis("8BF4"));
        }
        public void mov_esp_ebp()
        {
            Add(_asm, Analysis("8BE5"));
        }
        public void mov_esp_ebx()
        {
            Add(_asm, Analysis("8BE3"));
        }
        public void mov_esp_ecx()
        {
            Add(_asm, Analysis("8BE1"));
        }
        public void mov_esp_edi()
        {
            Add(_asm, Analysis("8BE7"));
        }
        public void mov_esp_edx()
        {
            Add(_asm, Analysis("8BE2"));
        }
        public void mov_esp_esi()
        {
            Add(_asm, Analysis("8BF6"));
        }
        public void mov_edi_ebp()
        {
            Add(_asm, Analysis("8BFD"));
        }
        public void mov_edi_ebx()
        {
            Add(_asm, Analysis("8BFB"));
        }
        public void mov_edi_ecx()
        {
            Add(_asm, Analysis("8BF9"));
        }
        public void mov_edi_edx()
        {
            Add(_asm, Analysis("8BFA"));
        }
        public void mov_edi_esp()
        {
            Add(_asm, Analysis("8BFC"));
        }
        public void mov_ebp_edi()
        {
            Add(_asm, Analysis("8BDF"));
        }
        public void mov_ebp_ebx()
        {
            Add(_asm, Analysis("8BEB"));
        }
        public void mov_ebp_ecx()
        {
            Add(_asm, Analysis("8BE9"));
        }
        public void mov_ebp_edx()
        {
            Add(_asm, Analysis("8BEA"));
        }
        public void mov_ebp_esi()
        {
            Add(_asm, Analysis("8BEE"));
        }
        public void mov_ebp_esp()
        {
            Add(_asm, Analysis("8BEC"));
        }
        public void push(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("6A")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("68")), BitConverter.GetBytes(a));
        }
        public void push_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("FF35")), BitConverter.GetBytes(a));
        }
        public void push_eax()
        {
            Add(_asm, Analysis("50"));
        }
        public void push_ecx()
        {
            Add(_asm, Analysis("51"));
        }
        public void push_edx()
        {
            Add(_asm, Analysis("52"));
        }
        public void push_esp()
        {
            Add(_asm, Analysis("54"));
        }
        public void lea_eax_dword_ptr_eax_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D40")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D80")), BitConverter.GetBytes(a));
        }
        public void lea_eax_dword_ptr_ebx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D43")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D83")), BitConverter.GetBytes(a));
        }
        public void lea_eax_dword_ptr_ecx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D41")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D81")), BitConverter.GetBytes(a));
        }
        public void lea_eax_dword_ptr_edx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D42")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D82")), BitConverter.GetBytes(a));
        }
        public void lea_eax_dword_ptr_esi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D46")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D86")), BitConverter.GetBytes(a));
        }
        public void lea_eax_dword_ptr_esp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D4424")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D8424")), BitConverter.GetBytes(a));
        }
        public void lea_eax_dword_ptr_ebp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D45")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D85")), BitConverter.GetBytes(a));
        }
        public void lea_eax_dword_ptr_edi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D47")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D87")), BitConverter.GetBytes(a));
        }
        public void lea_ebx_dword_ptr_eax_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D58")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D98")), BitConverter.GetBytes(a));
        }
        public void lea_ebx_dword_ptr_ebx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D5B")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D9B")), BitConverter.GetBytes(a));
        }
        public void lea_ebx_dword_ptr_ecx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D59")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D99")), BitConverter.GetBytes(a));
        }
        public void lea_ebx_dword_ptr_edx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D5A")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D9A")), BitConverter.GetBytes(a));
        }
        public void lea_ebx_dword_ptr_edi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D5F")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D9F")), BitConverter.GetBytes(a));
        }
        public void lea_ebx_dword_ptr_ebp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D5D")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D9D")), BitConverter.GetBytes(a));
        }
        public void lea_ebx_dword_ptr_esi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D5E")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D9E")), BitConverter.GetBytes(a));
        }
        public void lea_ecx_dword_ptr_eax_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D48")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D88")), BitConverter.GetBytes(a));
        }
        public void lea_ecx_dword_ptr_ebx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D4B")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D8B")), BitConverter.GetBytes(a));
        }
        public void lea_ecx_dword_ptr_ecx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D49")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D89")), BitConverter.GetBytes(a));
        }
        public void lea_ecx_dword_ptr_edx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D4A")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D8A")), BitConverter.GetBytes(a));
        }
        public void lea_ecx_dword_ptr_edi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D4F")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D8F")), BitConverter.GetBytes(a));
        }
        public void lea_ecx_dword_ptr_ebp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D4D")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D8D")), BitConverter.GetBytes(a));
        }
        public void lea_ecx_dword_ptr_esi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D4E")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D8E")), BitConverter.GetBytes(a));
        }
        public void lea_edx_dword_ptr_eax_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D50")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D90")), BitConverter.GetBytes(a));
        }
        public void lea_edx_dword_ptr_ebx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D8D")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D53")), BitConverter.GetBytes(a));
        }
        public void lea_edx_dword_ptr_ecx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D51")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D91")), BitConverter.GetBytes(a));
        }
        public void lea_edx_dword_ptr_edx_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D52")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D92")), BitConverter.GetBytes(a));
        }
        public void lea_edx_dword_ptr_edi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D57")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D97")), BitConverter.GetBytes(a));
        }
        public void lea_edx_dword_ptr_ebp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D55")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D95")), BitConverter.GetBytes(a));
        }
        public void lea_edx_dword_ptr_esi_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8D56")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8D96")), BitConverter.GetBytes(a));
        }
        public void pop_ebx()
        {
            Add(_asm, Analysis("5B"));
        }
        public void pop_eax()
        {
            Add(_asm, Analysis("58"));
        }
        public void pop_ecx()
        {
            Add(_asm, Analysis("59"));
        }
        public void pop_edx()
        {
            Add(_asm, Analysis("5A"));
        }
        public void pop_esi()
        {
            Add(_asm, Analysis("5E"));
        }
        public void pop_esp()
        {
            Add(_asm, Analysis("5C"));
        }
        public void pop_edi()
        {
            Add(_asm, Analysis("5F"));
        }
        public void pop_ebp()
        {
            Add(_asm, Analysis("5D"));
        }
        public void mov_dword_ptr_eax_eax()
        {
            Add(_asm, Analysis("8900"));
        }
        public void mov_dword_ptr_eax_ebx()
        {
            Add(_asm, Analysis("8918"));
        }
        public void mov_dword_ptr_eax_ecx()
        {
            Add(_asm, Analysis("8908"));
        }
        public void mov_dword_ptr_eax_edx()
        {
            Add(_asm, Analysis("8910"));
        }
        public void mov_dword_ptr_ebx_eax()
        {
            Add(_asm, Analysis("8903"));
        }
        public void mov_dword_ptr_ebx_ebx()
        {
            Add(_asm, Analysis("891B"));
        }
        public void mov_dword_ptr_ebx_ecx()
        {
            Add(_asm, Analysis("890B"));
        }
        public void mov_dword_ptr_ebx_edx()
        {
            Add(_asm, Analysis("8913"));
        }
        public void mov_dword_ptr_ecx_eax()
        {
            Add(_asm, Analysis("8901"));
        }
        public void mov_dword_ptr_ecx_ebx()
        {
            Add(_asm, Analysis("8919"));
        }
        public void mov_dword_ptr_ecx_ecx()
        {
            Add(_asm, Analysis("8909"));
        }
        public void mov_dword_ptr_ecx_edx()
        {
            Add(_asm, Analysis("8911"));
        }
        public void mov_dword_ptr_edx_eax()
        {
            Add(_asm, Analysis("8902"));
        }
        public void mov_dword_ptr_edx_ebx()
        {
            Add(_asm, Analysis("891A"));
        }
        public void mov_dword_ptr_edx_ecx()
        {
            Add(_asm, Analysis("890A"));
        }
        public void mov_dword_ptr_edx_edx()
        {
            Add(_asm, Analysis("8912"));
        }
        public void sub_esp(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("83EC")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("81EC")), BitConverter.GetBytes(a));
        }
        public void mov_dword_ptr_esp(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("C70424")), BitConverter.GetBytes(a));
        }
        public void mov_dword_ptr_esp_add_eax(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("894424")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("898424")), BitConverter.GetBytes(a));
        }
        public void mov_dword_ptr_esp_add(string text1, string text2, int Type1 = 16, int Type2 = 16)
        {
            int a1 = 0, a2 = 0;
            if (Type1 == 16)
                a1 = int.Parse(text1, System.Globalization.NumberStyles.HexNumber);
            else
                a1 = int.Parse(text1);
            if (Type2 == 16)
                a2 = int.Parse(text2, System.Globalization.NumberStyles.HexNumber);
            else
                a2 = int.Parse(text2);
            if (a1 <= 127 && a1 >= -128)
                Add(Add(Add(_asm, Analysis("C74424")), BitConverter.GetBytes(a1)), BitConverter.GetBytes(a2));
            else
                Add(Add(Add(_asm, Analysis("C78424")), BitConverter.GetBytes(a1)), BitConverter.GetBytes(a2));
        }
        public void mov_dword_ptr_eax_add(string text1, string text2, int Type1 = 16, int Type2 = 16)
        {
            int a1 = 0, a2 = 0;
            if (Type1 == 16)
                a1 = int.Parse(text1, System.Globalization.NumberStyles.HexNumber);
            else
                a1 = int.Parse(text1);
            if (Type2 == 16)
                a2 = int.Parse(text2, System.Globalization.NumberStyles.HexNumber);
            else
                a2 = int.Parse(text2);
            if (a1 <= 127 && a1 >= -128)
                Add(Add(Add(_asm, Analysis("C740")), BitConverter.GetBytes(a1)), BitConverter.GetBytes(a2));
            else
                Add(Add(Add(_asm, Analysis("C780")), BitConverter.GetBytes(a1)), BitConverter.GetBytes(a2));
        }
        public void mov_dword_ptr_ecx_add_eax(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8941")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8981")), BitConverter.GetBytes(a));
        }
        public void xor_edi_edi()
        {
            Add(_asm, Analysis("33FF"));
        }
        public void mov_esi_dword_ptr_eax_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B40")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B80")), BitConverter.GetBytes(a));
        }
        public void jmp(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("E9")), BitConverter.GetBytes(a));
        }
        public void call(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("E8")), BitConverter.GetBytes(a));
        }
        public void mov_eax_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("A1")), BitConverter.GetBytes(a));
        }
        public void mov_edi_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("8B3D")), BitConverter.GetBytes(a));
        }
        public void mov_ebp_dword_ptr(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            Add(Add(_asm, Analysis("8B2D")), BitConverter.GetBytes(a));
        }
        public void mov_eax_dword_ptr_esp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B4424")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B8424")), BitConverter.GetBytes(a));
        }
        public void mov_ebx_dword_ptr_esp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B5C24")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B9C24")), BitConverter.GetBytes(a));
        }
        public void mov_ecx_dword_ptr_esp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B4C24")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B8C24")), BitConverter.GetBytes(a));
        }
        public void mov_edx_dword_ptr_esp_add(string text, int Type = 16)
        {
            int a = 0;
            if (Type == 16)
                a = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                a = int.Parse(text);
            if (a <= 127 && a >= -128)
                Add(Add(_asm, Analysis("8B5424")), BitConverter.GetBytes(a));
            else
                Add(Add(_asm, Analysis("8B9424")), BitConverter.GetBytes(a));
        }
        public void mov_ebx_dword_ptr_esp()
        {
            Add(_asm, Analysis("8B1C24"));
        }
        public void mov_ecx_dword_ptr_esp()
        {
            Add(_asm, Analysis("8B0C24"));
        }
        public void mov_ebx_ebp()
        {
            Add(_asm, Analysis("8BDD"));
        }
        public void mov_ecx_ebp()
        {
            Add(_asm, Analysis("8BCD"));
        }
        public void mov_edx_eax()
        {
            Add(_asm, Analysis("8BD0"));
        }
        public void mov_esi_eax()
        {
            Add(_asm, Analysis("8BF0"));
        }
        public void mov_esi_ebp()
        {
            Add(_asm, Analysis("8BF5"));
        }
        public void mov_esi_edx()
        {
            Add(_asm, Analysis("8BF2"));
        }
        public void mov_esi_esi()
        {
            Add(_asm, Analysis("8BF6"));
        }
        public void mov_esp_eax()
        {
            Add(_asm, Analysis("8BE0"));
        }
        public void mov_esp_esp()
        {
            Add(_asm, Analysis("8BE4"));
        }
        public void mov_eax_eax()
        {
            Add(_asm, Analysis("8BC0"));
        }
        public void mov_ebx_ebx()
        {
            Add(_asm, Analysis("8BDB"));
        }
        public void mov_ecx_ecx()
        {
            Add(_asm, Analysis("8BC9"));
        }
        public void mov_edx_edx()
        {
            Add(_asm, Analysis("8BD2"));
        }
        public void mov_edi_eax()
        {
            Add(_asm, Analysis("8BF8"));
        }
        public void mov_edi_edi()
        {
            Add(_asm, Analysis("8BFF"));
        }
        public void mov_ebp_eax()
        {
            Add(_asm, Analysis("8BE8"));
        }
        public void mov_ebp_ebp()
        {
            Add(_asm, Analysis("8BED"));
        }
        public void push_ebp()
        {
            Add(_asm, Analysis("55"));
        }
        public void push_ebx()
        {
            Add(_asm, Analysis("53"));
        }
        public void push_esi()
        {
            Add(_asm, Analysis("56"));
        }
        public void push_edi()
        {
            Add(_asm, Analysis("57"));
        }




        public void xor_eax_eax()
        {
            Add(_asm, Analysis("33C0"));
        }
        public void xor_ebx_ebx()
        {
            Add(_asm, Analysis("33DB"));
        }
        public void xor_ecx_ecx()
        {
            Add(_asm, Analysis("33C9"));
        }
        public void xor_edx_edx()
        {
            Add(_asm, Analysis("33D2"));
        }
        public void xor_eax_ebx()
        {
            Add(_asm, Analysis("33C3"));
        }
        public void xor_eax_ecx()
        {
            Add(_asm, Analysis("33C1"));
        }
        public void xor_eax_edx()
        {
            Add(_asm, Analysis("33C2"));
        }
        public void xor_ebx_eax()
        {
            Add(_asm, Analysis("33D8"));
        }
        public void xor_ebx_ecx()
        {
            Add(_asm, Analysis("33D9"));
        }
        public void xor_ebx_edx()
        {
            Add(_asm, Analysis("33DA"));
        }
        public void xor_ecx_eax()
        {
            Add(_asm, Analysis("33C8"));
        }
        public void xor_ecx_ebx()
        {
            Add(_asm, Analysis("33CB"));
        }
        public void xor_ecx_edx()
        {
            Add(_asm, Analysis("33CA"));
        }
    }
}
