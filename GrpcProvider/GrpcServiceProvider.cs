using Grpc.Net.Client;
using GrpcService;
using System;

namespace GrpcProvider
{
    public class GrpcServiceProvider : IDisposable
    {
        /*public GrpcServiceProvider()
        {
            this.ServiceUrl = "https://localhost:5001";
            this.DefaultRpcChannel = new Lazy<GrpcChannel>(GrpcChannel.ForAddress(this.ServiceUrl));
        }

        public Greeter.GreeterClient GetGreeterClient() => this.GreeterClient ??= new Greeter.GreeterClient(this.DefaultRpcChannel.Value);

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this.DefaultRpcChannel.IsValueCreated)
                    {
                        this.DefaultRpcChannel.Value.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GrpcServiceProvider()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support

        private Lazy<GrpcChannel> DefaultRpcChannel { get; set; }
        private string ServiceUrl { get; set; }
        private Greeter.GreeterClient GreeterClient { get; set; }*/
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
