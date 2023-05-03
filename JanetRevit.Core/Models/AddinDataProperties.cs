using System;
using System.ComponentModel;
using Autodesk.Revit.DB;
using JanetRevit.Core.RvtTasks;

namespace JanetRevit.Core.Models
{
    public class AddinDataProperties : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string SystemUsername { get; set; }
        public RvtTask RvtTask { get; set; }
        public Document LoadedDocument { get; set; }
        public EventHandler OnDocumentSwitched { get; set; }
        public EventHandler OnDocumentOpened { get; set; }
        public void OnPropertyChanged(string param)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(param));
        }
        
        public void Application_DocumentSwitched(object sender, Autodesk.Revit.UI.Events.ViewActivatedEventArgs e)
        {
            LoadedDocument = e.Document;

            if ((e.PreviousActiveView != null) && (e.PreviousActiveView.Document != null))
            {
                if (!e.PreviousActiveView.Document.Equals(e.CurrentActiveView.Document))
                {
                    OnDocumentSwitched?.Invoke(this, e);
                }
            }
            else
            {
                OnDocumentOpened?.Invoke(this, e);
            }
        }
    }
}
