namespace CatelDemo1.ViewModels
{
    using Catel.IoC;
    using Catel.MVVM;
    using Catel.Services;
    using Catel.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class MainWindowViewModel : ViewModelBase
    {
        //поле для получения сервиса, которым будем запускать диалоговое окно
        private readonly IUIVisualizerService mIUIVisualizerService;
        //поле для получения сервиса, которым будем запускать окно сообщения
        private readonly IMessageService mIMessageService;
        //IUIVisualizerService uiVisualizerService = ServiceLocator.Default.ResolveType<IUIVisualizerService>();

        //поле для коллекции слайдов
        private List<IViewModel> mSlidesList;

        //свойство зависимости для приема текущего слайда
        public IViewModel currentSlide
        {
            get { return GetValue<IViewModel>(currentSlideProperty); }
            set { SetValue(currentSlideProperty, value); }
        }

        public static readonly PropertyData currentSlideProperty = RegisterProperty("currentSlide", typeof(IViewModel), null);

        public MainWindowViewModel(IUIVisualizerService _iUIVisualizerService, IMessageService _imessageService)
        {
            //инициализируем и регистрируем сервисы

            mIUIVisualizerService = _iUIVisualizerService;
            mIUIVisualizerService.Register(typeof(DialogViewModel), typeof(DialogView));

            mIMessageService = _imessageService;

            //наполняем коллекцию слайдами
            mSlidesList = new List<IViewModel>
            {
                new Slide1ViewModel(),
                new Slide2ViewModel(),
                new Slide3ViewModel()
            };

            //при загрузке окна по умолчанию загружаем первый слайд
            currentSlide = mSlidesList[0];
        }

        public override string Title { get { return "CatelDemo1"; } }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets
        
        //команда для вызова обработчика клика по кнопке
        public Command NextSlideCommand
        {
            get
            {
                return new Command(() =>
                {
                    //сохраняем индекс текущего слайда
                    var currentSlideIdx = mSlidesList.IndexOf(currentSlide);
                    //увеличиваем значение индекса на единицу, затем проверяем его:
                    //если текущий индекс - максимально возможный,
                    //то обнуляем его значение, замыкая цикл перебора слайдов
                    if(++currentSlideIdx == mSlidesList.Count)
                    {
                        currentSlideIdx = 0;
                    }
                    //устанавливаем некотороый слайд из коллекции в качестве текущего
                    //согласно полученного индекса
                    currentSlide = mSlidesList[currentSlideIdx];
                });
            }
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }
    }
}
