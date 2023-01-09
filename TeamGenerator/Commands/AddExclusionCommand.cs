﻿using System;
using System.Windows.Input;
using TeamGenerator.MVVM.ViewModels;

namespace TeamGenerator.Commands
{
    public class AddExclusionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter is EditPlayersViewModel vm)
            {
                if (vm.SelectedRelatedPlayer is not null)
                    if (!vm.SelectedRelatedPlayer.IsInclusionOfSelectedPlayer
                        && !vm.SelectedRelatedPlayer.IsExclusionOfSelectedPlayer
                        && !vm.SelectedPlayer.Equals(vm.SelectedRelatedPlayer))
                        return true;

                return false;
            }

            if (parameter is null) // in case this is called before the DataContext is created
                return false;

            throw new InvalidOperationException("Something went horribly wrong!");
        }

        public void Execute(object? parameter)
        {
            if (parameter is EditPlayersViewModel vm)
            {
                vm.SelectedPlayer.AddExclusion(vm.SelectedRelatedPlayer);

                vm.SelectedRelatedPlayer.IsRelationOfSelectedPlayer = true;
                vm.SelectedRelatedPlayer.IsExclusionOfSelectedPlayer = true;

                return;
            }

            throw new InvalidOperationException("Something went horribly wrong!");
        }
    }
}
