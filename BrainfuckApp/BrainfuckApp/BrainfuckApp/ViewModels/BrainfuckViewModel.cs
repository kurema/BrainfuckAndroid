using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using System.Threading;

namespace BrainfuckApp.ViewModels
{
    public class BrainfuckViewModel : BaseViewModel
    {
        //https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/concepts/async/cancel-async-tasks-after-a-period-of-time
        //https://qiita.com/mxProject/items/ecbbd74720d76fa7b1f2
        CancellationTokenSource cancellationTokenSourceEngine = new CancellationTokenSource();

        public BrainfuckViewModel()
        {
        }

        //Note: StringBuilder doesn't improve performance here because editor is binding this.
        private string _Code = string.Empty;

        public string Code {
            get => _Code; set => SetProperty(ref _Code, value);
        }

        private string _Input = "";
        public string Input {
            get => _Input; set => SetProperty(ref _Input, value);
        }

        private string _Output = "";
        public string Output {
            get => _Output;
        }

        private bool _IsOutputLatest = false;
        public bool IsOutputLatest {
            get => _IsOutputLatest; private set => SetProperty(ref _IsOutputLatest, value);
        }

        private System.Windows.Input.ICommand? _CodeAppendCommand;
        public System.Windows.Input.ICommand CodeAppendCommand => _CodeAppendCommand = _CodeAppendCommand ?? new Command((parameter) => {
            var paramString = parameter?.ToString();
            if (string.IsNullOrEmpty(paramString)) return;
            Code += paramString;
        });

        private System.Windows.Input.ICommand? _CodeClearCommand;
        public System.Windows.Input.ICommand CodeClearCommand => _CodeClearCommand = _CodeClearCommand ?? new Command((parameter) => { Code = string.Empty; });


        private System.Windows.Input.ICommand? _CodeBackspaceCommand;
        public System.Windows.Input.ICommand CodeBackspaceCommand => _CodeBackspaceCommand = _CodeBackspaceCommand ?? new Command((parameter) => {
            if (Code.Length == 0) return;
            Code = Code.Substring(0, Code.Length - 1);
        });

        public static string ExecuteSimple(string code, string standardIn)
        {
            var option = new BrainfuckRunner.Library.BfEngineOptions() {
                Input = new System.IO.StringReader(standardIn),
            };
            var engine = new BrainfuckRunner.Library.BfEngine(option);
            engine.ExecuteScript(code);
            return engine.Output.ToString();
        }
    }
}
