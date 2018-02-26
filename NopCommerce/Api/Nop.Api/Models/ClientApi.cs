using Nop.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Nop.Api.Models
{
    public class ClientContext
    {
        private static ClientContext _instance;
        private static List<ClientApi> _clientApis;
        public static ClientContext Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClientContext();
                return _instance;
            }
        }
        private ClientContext()
        {
            var path = Path.Combine(CommonHelper.MapPath("~/App_Data/"), "clientApis.xml");
            XElement xelement = XElement.Load(path);
            IEnumerable<XElement> clients = xelement.Elements();
            // Read the entire XML
            _clientApis = new List<ClientApi>();
            foreach (var client in clients)
            {
                var clientApi = new ClientApi()
                {
                    Id = Int32.Parse(client.Attribute("Id").Value),
                    ClientName = client.Element("ClientName").Value,
                    ClientSecret = client.Element("ClientSecret").Value,
                    ClientId = client.Element("ClientId").Value,
                    Roles = client.Element("Roles").Value
                };
                _clientApis.Add(clientApi);
            }
            
        }
        public List<ClientApi> ClientApis { get { return _clientApis; } }
        public static bool ValidateClient(string clientId, string clientSecret)
        {
            var client = Instance.ClientApis.FirstOrDefault(x => x.ClientId == clientId && x.ClientSecret == clientSecret);
            return client != null;
        }
        public static ClientApi FindClient(string clientId)
        {
            return Instance.ClientApis.FirstOrDefault(x => x.ClientId == clientId);
        }
    }

    public class ClientApi
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ClientName { get; set; }
        public string Roles { get; set; }
    }
}