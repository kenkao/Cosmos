﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Indy.IL2CPU.Assembler;
using Indy.IL2CPU.IL;

namespace Indy.IL2CPU.IL {
	public abstract class OpCodeMap {
		protected readonly SortedList<OpCodeEnum, Type> mMap = new SortedList<OpCodeEnum, Type>();
        protected readonly SortedList<OpCodeEnum, Action<ILReader, MethodInformation, SortedList<string, object>, IServiceProvider>> mScanMethods = new SortedList<OpCodeEnum, Action<ILReader, MethodInformation, SortedList<string, object>, IServiceProvider>>();

		protected OpCodeMap() {
			MethodHeaderOp = GetMethodHeaderOp();								   
			MethodFooterOp = GetMethodFooterOp();
			CustomMethodImplementationProxyOp = GetCustomMethodImplementationProxyOp();
			CustomMethodImplementationOp = GetCustomMethodImplementationOp();
			InitVmtImplementationOp = GetInitVmtImplementationOp();
			MainEntryPointOp = GetMainEntryPointOp();
		}

		protected abstract Assembly ImplementationAssembly {
			get;
		}

        public void SetServiceProvider(IServiceProvider aProvider)
        {
            mServiceProvider = aProvider;
        }

        protected IServiceProvider GetServiceProvider()
        {
            return mServiceProvider;
        }

        private IServiceProvider mServiceProvider;

        protected T GetService<T>()
        {
            if (mServiceProvider == null)
            {
                throw new Exception("No ServiceProvider specified!");
            }
            return mServiceProvider.GetService<T>();
        }

		protected abstract Type GetMethodHeaderOp();
		protected abstract Type GetMethodFooterOp();
		protected abstract Type GetCustomMethodImplementationProxyOp();
		protected abstract Type GetCustomMethodImplementationOp();
		protected abstract Type GetInitVmtImplementationOp();
		protected abstract Type GetMainEntryPointOp();

        public void ScanILCode(ILReader aReader, MethodInformation aMethod, SortedList<string, object> aMethodData) {
            if(mScanMethods.ContainsKey(aReader.OpCode)) {
                mScanMethods[aReader.OpCode](aReader,
                                             aMethod,
                                             aMethodData,
                                             GetServiceProvider());
            }
        }

	    public virtual void Initialize(IEnumerable<Assembly> aApplicationAssemblies) {
			foreach (var xItem in (from item in ImplementationAssembly.GetTypes()
								   let xAttrib = item.GetCustomAttributes(typeof(OpCodeAttribute), true).FirstOrDefault() as OpCodeAttribute
								   where item.IsSubclassOf(typeof(Op)) && xAttrib != null
								   select new {
									   OpCode = xAttrib.OpCode,
									   Type = item
								   })) {
				try {
					mMap.Add(xItem.OpCode, xItem.Type);
				    var xMethod = xItem.Type.GetMethod("ScanOp",
				                         new Type[] {typeof(ILReader), typeof(MethodInformation), typeof(SortedList<string, object>), typeof(IServiceProvider)});
                    if (xMethod != null)
                    {
                        mScanMethods.Add(xItem.OpCode,
                                         (Action<ILReader, MethodInformation, SortedList<string, object>, IServiceProvider>)Delegate.CreateDelegate(typeof(Action<ILReader, MethodInformation, SortedList<string, object>, IServiceProvider>),
                                                                                                                                  xMethod));
                    }
                    else
                    {
                        // todo: remove:
                        if (xItem.Type.GetMethod("ScanOp") != null)
                        {
                            throw new Exception("Class '" + xItem.Type.FullName +
                                                "' has a ScanOp method with wrong signature!");
                        }
                    }
				} catch {
					Console.WriteLine("Was adding op " + xItem.OpCode);
					throw;
				}
			}
		}

		public Type GetOpForOpCode(OpCodeEnum code) {
			if (!mMap.ContainsKey(code)) {
				throw new NotSupportedException("OpCode '" + code + "' not supported!");
			}
			return mMap[code];
		}

		public readonly Type MethodHeaderOp;
		public readonly Type MethodFooterOp;
		public readonly Type CustomMethodImplementationProxyOp;
		public readonly Type CustomMethodImplementationOp;
		public readonly Type InitVmtImplementationOp;
		public readonly Type MainEntryPointOp;
		
		public virtual Type GetOpForCustomMethodImplementation(string aName) {
			return null;
		}

		public virtual IList<Assembly> GetPlugAssemblies() {
			var xResult = new List<Assembly> {
			                                     typeof(OpCodeMap).Assembly,
			                                     Assembly.Load("Indy.IL2CPU")
			                                 };
		    return xResult;
		}

		public MethodBase GetCustomMethodImplementation(string aOrigMethodName) {
			return null;
		}

		public virtual bool HasCustomAssembleImplementation(MethodInformation aMethod) {
            //PlugMethodAttribute xResult = ((PlugMethodAttribute[])aMethod.Method.GetCustomAttributes(typeof(PlugMethodAttribute), true)).FirstOrDefault();
            //if (xResult != null) {
            //    return xResult.Assembler != null;
            //}
            //return false;
            throw new NotImplementedException();
		}

        public virtual void ScanCustomAssembleImplementation(MethodInformation aMethod) {
        }

	    public virtual void DoCustomAssembleImplementation(Assembler.Assembler aAssembler, MethodInformation aMethodInfo) {
            //PlugMethodAttribute xAttrib = aMethodInfo.Method.GetCustomAttributes(typeof(PlugMethodAttribute), true).Cast<PlugMethodAttribute>().FirstOrDefault();
            //if (xAttrib != null) {
            //    Type xAssemblerType = xAttrib.Assembler;
            //    if (xAssemblerType != null) {
            //        var xAssembler = (AssemblerMethod)Activator.CreateInstance(xAssemblerType);
            //        var xNeedsMethodInfo = xAssembler as INeedsMethodInfo;
            //        if (xNeedsMethodInfo != null) {
            //            xNeedsMethodInfo.MethodInfo = aMethodInfo; }
            //        xAssembler.Assemble(aAssembler);
            //    }
            //}
            throw new NotImplementedException();
		} 

		public virtual void PostProcess(Assembler.Assembler aAssembler) {
		}

		public abstract void EmitOpDebugHeader(Assembler.Assembler aAssembler, uint aOpId, string aOpLabel);

        public virtual void RegisterAllUtilityMethods() {
        }

	    public virtual void PreProcess(Indy.IL2CPU.Assembler.Assembler mAssembler)
        {
            
        }
    }
}