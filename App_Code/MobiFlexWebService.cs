using System;
using System.Collections;
using System.Xml;
// Next ones are for the reflection
using System.Reflection;

   #region MobiFlexWebService class
    /// <summary>
    /// Class for encapsulating a web service element
    /// </summary>
    public class MobiFlexWebService 
    {
        #region Member variables
        private string name;
        private string wsdlURL;
        private string username;
        private string password;
        private string domain;
        private string method;
        private ArrayList inputs;
        private ArrayList outputs;
        private ArrayList web_methods;
        #endregion

        #region Constructors
        /// <summary>
        /// Craete a new instance of a called web service.
        /// </summary>
        /// <param name="name">The name of the web service.</param>
        /// <param name="wsdlURL">The WSDL URL.</param>
        /// <param name="username">The username required to access the WSDL.</param>
        /// <param name="password">The password required to access the WSDL.</param>
        /// <param name="domain">The domain required to access the WSDL.</param>
        /// <param name="method">The method used on the web service.</param>
        /// <param name="inputs">An arraylist containing the inputs.</param>
        /// <param name="outputs">An arraylist containing the outputs.</param>
        public MobiFlexWebService(string name, string wsdlURL,
            string username, string password, string domain, string method,
            ArrayList inputs, ArrayList outputs, ArrayList web_methods)
        {

            this.name = name;
            this.wsdlURL = wsdlURL;
            this.username = username;
            this.password = password;
            this.domain = domain;
            this.method = method;
            this.web_methods = web_methods;
            this.inputs = inputs;
            this.outputs = outputs;
        }
        #endregion

        #region Properties

        public ArrayList Inputs
        {
            get { return this.inputs; }
            set { this.inputs = value; }
        }

        public ArrayList Outputs
        {
            get { return this.outputs; }
            set { this.outputs = value; }
        }

        public string WSDLURL
        {
            get { return this.wsdlURL; }
            set { this.wsdlURL = value; }
        }

        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }

        public string Domain
        {
            get { return this.domain; }
            set { this.domain = value; }
        }
        
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public string Method
        {
            get { return this.method; }
            set { this.method = value; }
        }

        public ArrayList WebMethods
        {
            get { return this.web_methods; }
            set { this.web_methods = value; }
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Clone the current web service
        /// </summary>
        /// <returns></returns>
       /* public override MCMLanguageElement Clone()
        {

            ArrayList _inputs = new ArrayList();
            ArrayList _outputs = new ArrayList();

            foreach (WebServiceInput _input in this.inputs)
            {
                _inputs.Add(new WebServiceInput(
                    _input.ParamName, _input.ParamType, _input.WebServiceName));
            }

            foreach (WebServiceOutput _output in this.outputs)
            {
                _outputs.Add(new WebServiceOutput(
                    _output.ParamName, _output.ParamType, _output.XPath, _output.Indent));
            }

            return new MobiFlexWebService(this.name, this.wsdlURL, this.username, this.password,
                this.domain, this.method, _inputs, _outputs, this.web_methods, this.mcmFile);
        }*/
        #endregion

        #region Methods
        #endregion
    }
    #endregion

    #region WebServiceInput class
    /// <summary>
    /// Class for encapsulating an input to a web service.
    /// </summary>
    public class WebServiceInput
    {

        #region Members
        private string paramName;
        private string paramType;
        private string webServiceName;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new web service input.
        /// </summary>
        /// <param name="paramName">The MCM name of the parameter</param>
        /// <param name="parameterType">The MCM type of the parameter</param>
        /// <param name="webServiceName">The name of the web service parameter.</param>
        public WebServiceInput(string paramName,
            string paramType, string webServiceName)
        {

            this.paramName = paramName;
            this.paramType = paramType;
            this.webServiceName = webServiceName;
        }
        #endregion

        #region Properties
        public string ParamName
        {
            get { return this.paramName; }
        }

        public string ParamType
        {
            get { return this.paramType; }
        }

        public string WebServiceName
        {
            get { return this.webServiceName; }
            set { this.webServiceName = value; }
        }
        #endregion
    }
    #endregion

    #region WebServiceOutput class
    /// <summary>
    /// Class for encapsulating an output to a web service.
    /// </summary>
    public class WebServiceOutput
    {

        #region Member variables
        private string paramName;
        private string paramType;
        private string xpath;
        private int indent;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new web service output param.
        /// </summary>
        /// <param name="paramName">The MCM name of the parameter.</param>
        /// <param name="paramType">The MCM type of the parameter.</param>
        /// <param name="xpath">The xpath to the return variable.</param>
        /// <param name="indent">The indent level of the output.</param>
        public WebServiceOutput(string paramName,
            string paramType, string xpath, int indent)
        {

            this.paramName = paramName;
            this.paramType = paramType;
            this.xpath = xpath;
            this.indent = indent;
        }
        #endregion

        #region Properties

        public string ParamName
        {
            get { return this.paramName; }
        }

        public string ParamType
        {
            get { return this.paramType; }
        }

        public string XPath
        {
            get { return this.xpath; }
            set { this.xpath = value; }
        }

        public int Indent
        {
            get { return this.indent; }
        }
        #endregion
    }
    #endregion

    #region WebMethod class
    /// <summary>
    /// Class to encapsulate a web method parsed from the 
    /// ParseWSDL response service.
    /// </summary>
    public class WebMethod
    {

        #region Member variables
        private string name;
        private string type;
        private ArrayList inputs;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a web method
        /// </summary>
        /// <param name="name">The name of the web method</param>
        /// <param name="inputs">The inputs for the web method</param>
        public WebMethod(string name, string type, ArrayList inputs)
        {

            this.name = name;
            this.type = type;
            this.inputs = inputs;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The name of the web method
        /// </summary>
        public string Name { get { return this.name; } }
        public string Type { get { return this.type; } }
        /// <summary>
        /// The array list of inputs
        /// </summary>
        public ArrayList Inputs { get { return this.inputs; } }

        #endregion
    }
    #endregion


