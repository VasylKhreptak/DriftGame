using System;
using Infrastructure.Services.PersistentData.Core;
using UI.Texts.Core;
using UniRx;
using Zenject;

namespace UI.Texts
{
    public class CoinsText : ReactiveText
    {
        private IPersistentDataService _persistentData;

        [Inject]
        private void Constructor(IPersistentDataService persistentDataService)
        {
            _persistentData = persistentDataService;
        }

        protected override IObservable<string> GetObservable() => _persistentData.Data.PlayerData.Coins.Amount.Select(x => x.ToString());
    }
}