using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Common.Controls;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Helpers;
using Izi.Travel.Core.Services;
using Izi.Travel.Mtg.ViewModels.Quiz;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Izi.Travel.Mtg.Commands
{
    /// <summary>
    /// Command to handle opening a quiz
    /// </summary>
    public class OpenQuizCommand : BaseCommand
    {
        private readonly MtgObject _mtgObject;
        private readonly MtgObject _mtgObjectRoot;

        /// <summary>
        /// Gets a value indicating whether the quiz is available
        /// </summary>
        public bool HasQuiz { get; }

        /// <summary>
        /// Initializes a new instance of the OpenQuizCommand class
        /// </summary>
        /// <param name="mtgObject">The MTG object containing the quiz</param>
        /// <param name="mtgObjectRoot">The root MTG object</param>
        public OpenQuizCommand(MtgObject mtgObject, MtgObject mtgObjectRoot) : base(null)
        {
            _mtgObject = mtgObject ?? throw new ArgumentNullException(nameof(mtgObject));
            _mtgObjectRoot = mtgObjectRoot ?? throw new ArgumentNullException(nameof(mtgObjectRoot));
            HasQuiz = _mtgObject?.MainContent?.Quiz != null;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter) => HasQuiz;

        /// <inheritdoc/>
        protected override Task OnExecuteAsync(object parameter)
        {
            if (!HasQuiz || !PurchaseFlyoutDialog.ConditionalShow(_mtgObjectRoot))
                return Task.CompletedTask;

            PhoneStateHelper.SetParameter("MtgObjectFull", _mtgObject);
            
            ShellServiceFacade.NavigationService
                .UriFor<QuizPartViewModel>()
                .WithParam(x => x.Uid, _mtgObject.Uid)
                .WithParam(x => x.Language, _mtgObject.Language)
                .Navigate();

            return Task.CompletedTask;
        }
    }
}
