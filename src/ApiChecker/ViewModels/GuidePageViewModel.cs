﻿using System;
using System.Collections.Generic;
using ApiChecker.Helpers;
using ApiChecker.Models;
using ApiChecker.Repository.Interfaces;
using ApiChecker.Views;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using Xamarin.Forms;

namespace ApiChecker.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GuidePageViewModel : ViewModelBase
    {
        public List<TutorialItemModel> TutorialGuides { get; set; }
        public DelegateCommand CloseGuideCommand { get; set; }

        private readonly ITutorialRepository _tutorialRepository;

        public GuidePageViewModel(INavigationService navigationService,
                                  ITutorialRepository tutorialRepository) :
            base(navigationService)
        {
            _tutorialRepository = tutorialRepository;
            TutorialGuides = new List<TutorialItemModel>();
            CloseGuideCommand = new DelegateCommand(CloseCommandAction);
        }

        public void CloseCommandAction()
        {
            Settings.IsTutorialCompleted = true;
            NavigationService.NavigateAsync(nameof(NavigationPage) + "/" + nameof(MainPage));
        }


        public override void OnNavigatingTo(INavigationParameters parameters)
		{
            base.OnNavigatingTo(parameters);
            TutorialGuides = new List<TutorialItemModel>(_tutorialRepository.GetTutorialGuides());
		}
	}
}
