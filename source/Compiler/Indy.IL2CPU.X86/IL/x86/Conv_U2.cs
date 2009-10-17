//using System;

//using CPUx86 = Indy.IL2CPU.Assembler.X86;
//using Indy.IL2CPU.Assembler;

//namespace Indy.IL2CPU.IL.X86 {
//    [OpCode(OpCodeEnum.Conv_U2)]
//    public class Conv_U2: Op {
//        private string mNextLabel;
//        private string mCurLabel;
//        private uint mCurOffset;
//        private MethodInformation mMethodInformation;
//        public Conv_U2(ILReader aReader, MethodInformation aMethodInfo)
//            : base(aReader, aMethodInfo) {
//             mMethodInformation = aMethodInfo;
//            mCurOffset = aReader.Position;
//            mCurLabel = IL.Op.GetInstructionLabel(aReader);
//            mNextLabel = IL.Op.GetInstructionLabel(aReader.NextPosition);
//        }
//        public override void DoAssemble() {
//            if (Assembler.StackContents.Peek().IsFloat) {
//                EmitNotImplementedException(Assembler, GetServiceProvider(), "Conv_U2: Floats not yet supported", mCurLabel, mMethodInformation, mCurOffset, mNextLabel);
//                return;
//            }
//            int xSource = Assembler.StackContents.Pop().Size;
//            switch (xSource) {
//                case 1:
//                case 4: {
//                        new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
//                        new CPUx86.Push { DestinationReg = CPUx86.Registers.EAX };
//                        break;
//                    }
//                case 8: {
//                        new CPUx86.Pop{DestinationReg = CPUx86.Registers.EAX};
//                        new CPUx86.Pop { DestinationReg = CPUx86.Registers.ECX };
//                        new CPUx86.Push{DestinationReg=CPUx86.Registers.EAX};
//                        break;
//                    }
//                case 2: {
//                        new CPUx86.Noop();
//                        break;
//                    }
//                default:
//                    EmitNotImplementedException(Assembler, GetServiceProvider(), "Conv_U2: SourceSize " + xSource + " not yet supported!", mCurLabel, mMethodInformation, mCurOffset, mNextLabel);
//                    return;
//            }
//            Assembler.StackContents.Push(new StackContent(2, typeof(ushort)));
//        }
//    }
//}