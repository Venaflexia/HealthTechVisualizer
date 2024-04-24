using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthTechVisualizer.Mvvm.ViewModels
{
    public class DrawingVm:INotifyPropertyChanged
    {
        public ICommand CmdClearDrawingView { get; set; }
        private bool multiLineMode;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool MultiLineMode
        {
            get 
            {
                return multiLineMode; 
            }
            set 
            {
                multiLineMode = value;
                OnPropertyChanged(nameof(MultiLineMode)); 
            }
        }

        public void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }


        public DrawingVm()
        {
            CmdClearDrawingView = new Command(ClearClicked);
        }

        private void ClearClicked(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
