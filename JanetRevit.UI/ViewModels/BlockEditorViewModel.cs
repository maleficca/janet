using JanetRevit.Core.Handlers;
using JanetRevit.Core.Helpers;
using JanetRevit.Core.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using RevitSwitchAddin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace JanetRevit.UI.ViewModels
{
    public class BlockEditorViewModel : BindableBase
    {
        public event EventHandler<CodeChangedEventArgs> CodeChanged;
        public DelegateCommand RunTestCode => new DelegateCommand(RunBlock);

        private List<JanetBlock> _blocks;

        public List<JanetBlock> Blocks
        {
            get => _blocks;
            set { SetProperty(ref _blocks, value); }
        }

        private JanetBlock _selectedBlock;

        public JanetBlock SelectedBlock
        {
            get => _selectedBlock;
            set
            {
                SetProperty(ref _selectedBlock, value);
                CodeChanged?.Invoke(this, new CodeChangedEventArgs(SelectedBlock.Code));
            }
        }

        public BlockEditorViewModel()
        {
            LoadBlocks();
        }

        private void LoadBlocks()
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                Blocks = BlockManager.GetAllBlocks();
            });
        }

        private void RunBlock()
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                BaseRoslynScriptHandler handler = new BaseRoslynScriptHandler();
                handler.RunBlockCode(SelectedBlock.Code);
            });
        }

        public void SaveCode(string textEditorText)
        {
            SelectedBlock.Code = textEditorText;
            File.WriteAllText(SelectedBlock.FilePath, SelectedBlock.Code);
        }
    }
}