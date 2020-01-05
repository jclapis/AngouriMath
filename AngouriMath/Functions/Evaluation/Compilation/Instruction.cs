﻿using AngouriMath.Core;
using System;
using System.Collections.Generic;
using System.Text;
using static AngouriMath.CompiledMathFunctions;

namespace AngouriMath
{
    internal class Instruction
    {
        internal enum InstructionType
        {
            PUSHVAR,
            PUSHCONST,
            CALL,
            PULLCACHE,
            TOCACHE
        }
        internal InstructionType Type;
        internal int VarCount;
        internal int FuncNumber;
        internal int VarNumber;
        internal string FuncName;
        internal Number Value;
        internal int CacheNumber;

        /// <summary>
        /// CALL
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="varCount"></param>
        internal Instruction(string funcName, int varCount)
        {
            FuncName = funcName;
            FuncNumber = CompiledMathFunctions.func2Num[funcName];
            VarCount = varCount;
            Type = InstructionType.CALL;

            VarNumber = 0;
            Value = 0;
        }

        /// <summary>
        /// PUSH var's number
        /// </summary>
        /// <param name="varNumber"></param>
        internal Instruction(int varNumber)
        {
            VarNumber = varNumber;
            Type = InstructionType.PUSHVAR;
            
            FuncName = "";
            VarCount = 0;
            Value = 0;
            FuncNumber = 0;
        }

        /// <summary>
        /// PUSH CONST
        /// </summary>
        /// <param name="value"></param>
        internal Instruction(Number value)
        {
            Value = value;
            Type = InstructionType.PUSHCONST;

            FuncName = "";
            FuncNumber = 0;
            VarCount = 0;
            VarNumber = 0;
        }

        /// <summary>
        /// Cache instruction
        /// </summary>
        /// <param name="cacheRecordNumber"></param>
        /// <param name="ifPull"></param>
        internal Instruction(int cacheRecordNumber, bool ifPull)
        {
            CacheNumber = cacheRecordNumber;
            if (ifPull)
                Type = InstructionType.PULLCACHE;
            else
                Type = InstructionType.TOCACHE;
        }

        public override string ToString()
        {
            string b = Type.ToString() + " ";
            if (Type == InstructionType.CALL)
                return b + FuncName;
            else if (Type == InstructionType.PUSHCONST)
                return b + Value;
            else if (Type == InstructionType.PULLCACHE || Type == InstructionType.TOCACHE)
                return b + CacheNumber;
            else
                return b + VarNumber.ToString();
        }
    }
    internal class InstructionSet : List<Instruction>
    {
        internal void AddCallInstruction(string funcName, int varCount) 
            => Add(new Instruction(funcName, varCount));
        internal void AddPushVarInstruction(int varName)
            => Add(new Instruction(varName));
        internal void AddPushNumInstruction(Number value)
            => Add(new Instruction(value));
        internal void AddPullCacheInstruction(int cacheLine)
            => Add(new Instruction(cacheLine, true));
        internal void AddPushCacheInstruction(int cacheLine)
            => Add(new Instruction(cacheLine, false));
    }
}
