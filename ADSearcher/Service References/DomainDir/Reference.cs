﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ADSearcher.DomainDir {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://DomainDirectory/", ConfigurationName="DomainDir.DomainListServiceSoap")]
    public interface DomainListServiceSoap {
        
        // CODEGEN: Generating message contract since element name GetDomainDirectoryResult from namespace http://DomainDirectory/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://DomainDirectory/GetDomainDirectory", ReplyAction="*")]
        ADSearcher.DomainDir.GetDomainDirectoryResponse GetDomainDirectory(ADSearcher.DomainDir.GetDomainDirectoryRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://DomainDirectory/GetDomainDirectory", ReplyAction="*")]
        System.Threading.Tasks.Task<ADSearcher.DomainDir.GetDomainDirectoryResponse> GetDomainDirectoryAsync(ADSearcher.DomainDir.GetDomainDirectoryRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDomainDirectoryRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDomainDirectory", Namespace="http://DomainDirectory/", Order=0)]
        public ADSearcher.DomainDir.GetDomainDirectoryRequestBody Body;
        
        public GetDomainDirectoryRequest() {
        }
        
        public GetDomainDirectoryRequest(ADSearcher.DomainDir.GetDomainDirectoryRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetDomainDirectoryRequestBody {
        
        public GetDomainDirectoryRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDomainDirectoryResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDomainDirectoryResponse", Namespace="http://DomainDirectory/", Order=0)]
        public ADSearcher.DomainDir.GetDomainDirectoryResponseBody Body;
        
        public GetDomainDirectoryResponse() {
        }
        
        public GetDomainDirectoryResponse(ADSearcher.DomainDir.GetDomainDirectoryResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://DomainDirectory/")]
    public partial class GetDomainDirectoryResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDomainDirectoryResult;
        
        public GetDomainDirectoryResponseBody() {
        }
        
        public GetDomainDirectoryResponseBody(string GetDomainDirectoryResult) {
            this.GetDomainDirectoryResult = GetDomainDirectoryResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DomainListServiceSoapChannel : ADSearcher.DomainDir.DomainListServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DomainListServiceSoapClient : System.ServiceModel.ClientBase<ADSearcher.DomainDir.DomainListServiceSoap>, ADSearcher.DomainDir.DomainListServiceSoap {
        
        public DomainListServiceSoapClient() {
        }
        
        public DomainListServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DomainListServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DomainListServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DomainListServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ADSearcher.DomainDir.GetDomainDirectoryResponse ADSearcher.DomainDir.DomainListServiceSoap.GetDomainDirectory(ADSearcher.DomainDir.GetDomainDirectoryRequest request) {
            return base.Channel.GetDomainDirectory(request);
        }
        
        public string GetDomainDirectory() {
            ADSearcher.DomainDir.GetDomainDirectoryRequest inValue = new ADSearcher.DomainDir.GetDomainDirectoryRequest();
            inValue.Body = new ADSearcher.DomainDir.GetDomainDirectoryRequestBody();
            ADSearcher.DomainDir.GetDomainDirectoryResponse retVal = ((ADSearcher.DomainDir.DomainListServiceSoap)(this)).GetDomainDirectory(inValue);
            return retVal.Body.GetDomainDirectoryResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ADSearcher.DomainDir.GetDomainDirectoryResponse> ADSearcher.DomainDir.DomainListServiceSoap.GetDomainDirectoryAsync(ADSearcher.DomainDir.GetDomainDirectoryRequest request) {
            return base.Channel.GetDomainDirectoryAsync(request);
        }
        
        public System.Threading.Tasks.Task<ADSearcher.DomainDir.GetDomainDirectoryResponse> GetDomainDirectoryAsync() {
            ADSearcher.DomainDir.GetDomainDirectoryRequest inValue = new ADSearcher.DomainDir.GetDomainDirectoryRequest();
            inValue.Body = new ADSearcher.DomainDir.GetDomainDirectoryRequestBody();
            return ((ADSearcher.DomainDir.DomainListServiceSoap)(this)).GetDomainDirectoryAsync(inValue);
        }
    }
}
