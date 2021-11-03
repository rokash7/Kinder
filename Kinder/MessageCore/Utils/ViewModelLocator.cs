using Kinder.MessageCore.Services;
using Kinder.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Kinder.MessageCore.Utils
{
    public class ViewModelLocator
    {
        private UnityContainer container;

        public ViewModelLocator()
        {
            container = new UnityContainer();
            container.RegisterType<IChatService, ChatService>();
            container.RegisterType<IDialogService, DialogService>();
        }

        public MainViewModel MainVM
        {
            get { return container.Resolve<MainViewModel>(); }
        }
    }
}
