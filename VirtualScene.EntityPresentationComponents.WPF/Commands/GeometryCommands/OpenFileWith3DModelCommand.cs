﻿using Microsoft.Win32;
using VirtualScene.EntityPresentationComponents.WPF.Properties;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;
using VirtualScene.PresentationComponents.WPF.Commands;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.GeometryCommands
{
    /// <summary>
    /// The command opening the dialog to choose the file with 3D viewModel
    /// </summary>
    public class OpenFileWith3DModelCommand: CommandBase
    {
        private readonly Import3DModelViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileWith3DModelCommand" />
        /// </summary>
        /// <param name="viewModel">The viewModel to keep state of the importing 3D viewModel operation</param>
        public OpenFileWith3DModelCommand(Import3DModelViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        const string OpenFileDialogFilter = "Wavefont Obj Files (*.obj)|*.obj";

        /// <summary>
        /// Executes the command to open the dialog to select a file with 3D viewModel
        /// </summary>
        /// <param name="parameter"></param>
        protected override void ExecuteAction(object parameter)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = OpenFileDialogFilter,
                Title = Resources.Title_Import3D_model
            };
            var dialogResult = fileDialog.ShowDialog()?? false;
            _viewModel.OperationCancelled = !dialogResult;
            if(_viewModel.OperationCancelled)
                return;
            _viewModel.FileName = fileDialog.FileName;
        }
    }
}