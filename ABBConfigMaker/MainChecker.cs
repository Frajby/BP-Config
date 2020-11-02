using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ABBConfigMaker
{
    class MainChecker
    {
        private bool hasError;
        private List<Delegate> checkers;
        public List<ErrorDataModel> errors;
        public MainChecker()
        {
            checkers = new List<Delegate>();
            errors = new List<ErrorDataModel>();
        }

        public void addChecker(Func<ErrorDataModel> checkMethod)
        {
            checkers.Add(checkMethod);
        }

        public bool checkAll()
        {
            foreach(Func<ErrorDataModel> checkMethod in checkers)
            {
                errors.Add(checkMethod());
            }

            string errMsg = string.Empty;

            foreach(ErrorDataModel error in errors)
            {
                if (error.isError)
                {
                    hasError = true;
                    errMsg += error.errorMessage + '\n';
                }
            }

            if (hasError)
            {
                MessageBox.Show(errMsg);
            }

            return hasError;
        }


    }
}
