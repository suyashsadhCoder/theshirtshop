using Acr.UserDialogs;
//using Android.Content;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
    public class RetailerRegistrationViewModel : ModelBase
    {
        private IImageResize _IImageResize;
        private RetailerMaster_Class RetailerMaster_Class_Data;
        private MultipleContect_Class MultipleContect_Class_Data;
        public ObservableCollection<MultipleImage_Class> Retailer_Secondar_Image { get; set; }
        public ObservableCollection<MultipleImage_Class> Retailer_Form_Image { get; set; }
        public ObservableCollection<MultipleContect_Class> Secondary_Mobile_No { get; set; }
        private IAllDataServices _IAllDataServices;
        private INavigation _INavigation { get; set; }
     
        public RetailerRegistrationViewModel(INavigation navigation)
        {
            _INavigation = navigation;
            RetailerMaster_Class_Data = new RetailerMaster_Class();
            MultipleContect_Class_Data = new MultipleContect_Class();
            Retailer_Secondar_Image = new ObservableCollection<MultipleImage_Class>();
            Retailer_Form_Image = new ObservableCollection<MultipleImage_Class>();
            Secondary_Mobile_No = new ObservableCollection<MultipleContect_Class>() {
                new MultipleContect_Class {
                    Contect_No=""
                }
            };
            _IImageResize = new ImageResize();
            SelectRetailerSinglePicCommand = new Command(async () => await SelectRetailerSinglePic());
            SelectRetailerMultiplePicCommand = new Command(async () => await SelectRetailerMultiplePic());
            SelectRetailerAharPicCommand = new Command(async () => await SelectRetailerAharPic());
            SelectRetailerMultipleFormPicCommand = new Command(async () => await SelectRetailerMultipleFormPic());

            SaveCommand = new Command(async () => await SaveData());
            _IAllDataServices = new AllDataServices();
            GetStateAsync();
          
        }
        public async Task GetStateAsync()
        {
            JObject result = await _IAllDataServices.GetStateAndCity();

            if (result != null)
            {
                List<StateMaster> sm = new List<StateMaster>();
                string type = result["Type"].ToString();
                if (type == "1")
                {
                    _State_Master_List = JsonConvert.DeserializeObject<List<StateMaster>>(result["Result"].ToString());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                }
            }
           
           
        }
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }
        private async Task SaveData()
        {
            try
            {
                if (HasErrors)
                {
                    // Error message
                    ScrollToControlProperty(GetFirstInvalidPropertyName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(RetailerMaster_Class_Data._Address) && !string.IsNullOrEmpty(RetailerMaster_Class_Data.GSTN_Adhar_No) && !string.IsNullOrEmpty(RetailerMaster_Class_Data.Mobile_No) && !string.IsNullOrEmpty(RetailerMaster_Class_Data.Retailer_Name) && RetailerMaster_Class_Data.City_Id != 0 && RetailerMaster_Class_Data.State_Id != 0 && _GSTN_Adhar_No_Image != null && _Retailer_Image != null )
                    {
                        var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
                        Wait.Show();
                        RetailerMaster_Class_Data.Employee_Id = (int)Application.Current.Properties["OtherId"];
                        RetailerMaster_Class_Data.Form_ImageByte = Retailer_Form_Image;
                        RetailerMaster_Class_Data.Retailer_RegistrationDate = DateTime.Now;
                        RetailerMaster_Class_Data._MultipleImage_ByteData = Retailer_Secondar_Image;
                        string AppendContectNo = string.Empty;
                        bool ProcessStatus = true;
                        foreach (var smn in Secondary_Mobile_No)
                        {

                            if (!string.IsNullOrEmpty(smn.Contect_No))
                            {
                                if (smn.ErrorMessageStatus == false)
                                {
                                    ProcessStatus = true;
                                    if (string.IsNullOrEmpty(AppendContectNo))
                                    {
                                        AppendContectNo = smn.Contect_No;
                                    }
                                    else
                                    {
                                        AppendContectNo += ',' + smn.Contect_No;
                                    }
                                }
                                else
                                {
                                    ProcessStatus = false;
                                    break;
                                }
                            }
                        }
                        if (ProcessStatus == true)
                        {
                            RetailerMaster_Class_Data.Secondary_Mobile_No = AppendContectNo;
                        JObject result = await _IAllDataServices.RetailerRegistration(RetailerMaster_Class_Data);

                            if (result != null)
                            {
                                string type = result["Type"].ToString();
                                if (type == "1")
                                {
                                    await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
                                    var ChackPriousPage = _INavigation.NavigationStack.Where(x => x.Title == "Retailer Registration").FirstOrDefault();

                                    if (ChackPriousPage != null)
                                    {

                                        _INavigation.RemovePage(ChackPriousPage);

                                    }
                                    await _INavigation.PushAsync(new RetailerRegistrationPage());
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");

                                }

                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Oops!", "Please Refresh Page And try Again....", "Ok");
                            }
                            
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Error!", "Minimum 10 and Maximum 12 Number Required in Alternate Number!", "Ok");
                        }
                        Wait.Dispose();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(_Retailer_Name))
                        {
                            _Retailer_Name = "";
                        }
                        else if (string.IsNullOrEmpty(_Address))
                        {
                            _Address = "";
                        }

                        else if (string.IsNullOrEmpty(_GSTN_Adhar_No))
                        {
                            _GSTN_Adhar_No = "";

                        }
                        else if (string.IsNullOrEmpty(_Primary_Mobile_No))
                        {
                            _Primary_Mobile_No = "";
                        }
                        else if (_Retailer_State_Id == 0)
                        {
                            _Retailer_State_Id = 0;
                            await App.Current.MainPage.DisplayAlert("Oops !", "Please Select State", "Ok");
                        }
                        else if (_Retailer_City_Id == 0)
                        {
                            _Retailer_City_Id = 0;
                            await App.Current.MainPage.DisplayAlert("Oops !", "Please Select City", "Ok");
                        }
                        else if (_GSTN_Adhar_No_Image == null)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops !", "Please Select Aadhar Image ", "Ok");
                        }
                        else if (_Retailer_Image == null)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops !", "Please Select Single Image ", "Ok");
                        }
                        else if (_Form_Image.Count == 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops !", "Please Select Form Image ", "Ok");
                        }



                    }
                }

            }
            catch (Exception ee)
            {

                await App.Current.MainPage.DisplayAlert("Error", ee.Message, "Ok");
            }


        }

        private async Task AddMoreContect()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if (_Secondary_Mobile_No.Count < 5)
            {
                Secondary_Mobile_No.Add(new MultipleContect_Class
                {
                    Contect_No = ""
                });
                _Secondary_Mobile_No = Secondary_Mobile_No;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Maximum five numbers can be added as alternetive contact number.....", "Ok");
            }
            Wait.Dispose();
        }

        private async Task SelectRetailerSinglePic()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });


            if (file == null)
            {
                return;
            }
            else
            {

                byte[] imageasbytes = null;
                using (var memorystream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memorystream);
                    file.Dispose();
                    imageasbytes = memorystream.ToArray();
                }

                if (imageasbytes.Length > 0)
                {
                    imageasbytes = _IImageResize.ResizeImage(imageasbytes, 300, 400, 100);
                    _Retailer_Image = imageasbytes;
                }
                Wait.Dispose();

            }

        }
        private async Task SelectRetailerMultiplePic()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });


            if (file == null)
            {
                return;
            }
            else
            {

                byte[] imageasbytes = null;
                using (var memorystream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memorystream);
                    file.Dispose();
                    imageasbytes = memorystream.ToArray();
                }

                if (imageasbytes.Length > 0)
                {
                    imageasbytes = _IImageResize.ResizeImage(imageasbytes, 300, 400, 100);
                    Retailer_Secondar_Image.Add(new MultipleImage_Class
                    {
                        Image = imageasbytes
                    });
                    _Retailer_Secondar_Image = Retailer_Secondar_Image;
                }
            }
            Wait.Dispose();

        }
        private async Task SelectRetailerAharPic()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });


            if (file == null)
            {
                return;
            }
            else
            {

                byte[] imageasbytes = null;
                using (var memorystream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memorystream);
                    file.Dispose();
                    imageasbytes = memorystream.ToArray();
                }

                if (imageasbytes.Length > 0)
                {
                    imageasbytes = _IImageResize.ResizeImage(imageasbytes, 300, 400, 100);
                    _GSTN_Adhar_No_Image = imageasbytes;
                }
            }
            Wait.Dispose();

        }
        private async Task SelectRetailerMultipleFormPic()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });


            if (file == null)
            {
                return;
            }
            else
            {

                byte[] imageasbytes = null;
                using (var memorystream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memorystream);
                    file.Dispose();
                    imageasbytes = memorystream.ToArray();
                }

                if (imageasbytes.Length > 0)
                {
                    imageasbytes = _IImageResize.ResizeImage(imageasbytes, 300, 400, 100);

                    Retailer_Form_Image.Add(new MultipleImage_Class
                    {
                        Image = imageasbytes
                    });

                    _Form_Image = Retailer_Form_Image;
                }
            }
            Wait.Dispose();

        }

        public ICommand SelectRetailerSinglePicCommand { get; }
        public ICommand SelectRetailerMultiplePicCommand { get; }
        public ICommand SelectRetailerAharPicCommand { get; }
        public ICommand SelectRetailerMultipleFormPicCommand { get; }
        public ICommand SaveCommand { get; }



        public ICommand AddMoreContectCommand { get { return new Command(async() => await AddMoreContect()); } }
        public Command<MultipleContect_Class> DeleteRowCommand
        {
            get
            {
                return new Command<MultipleContect_Class>((MultipleContect_Class) =>
                {

                    Secondary_Mobile_No.Remove(MultipleContect_Class);
                });
            }

        }

        public Command<int> DeleteRetailerSeconaryImageCommand
        {
            get
            {
                return new Command<int>((index) =>
                {

                    _Retailer_Secondar_Image.RemoveAt(index);
                });
            }

        }
        public Command<int> DeleteRetailerFormImageCommand
        {
            get
            {
                return new Command<int>((index) =>
                {

                    _Form_Image.RemoveAt(index);
                });
            }

        }



        private async void InitMedia()
        {
            await CrossMedia.Current.Initialize();
        }

        private Command _cameraCommand;
        public bool CanExecuteCameraCommand()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return false;
            }
            return true;
        }
        public ICommand GetRetailerSinglePicCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCameraSinglePicCommandAsync(), () => CanExecuteCameraCommand()); }
        }

        public ICommand GetRetailerMultiplePicCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCameraMultiplePicCommand(), () => CanExecuteCameraCommand()); }
        }
        public ICommand GetRetailerAdharPicCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCameraAdharPicCommand(), () => CanExecuteCameraCommand()); }
        }
        public ICommand GetRetailerFormPicCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCameraFormPicCommand(), () => CanExecuteCameraCommand()); }
        }

        private async Task ExecuteCameraFormPicCommand()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            InitMedia();
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());

            if (file == null)
            {
                return;
            }
            else
            {

                byte[] imageasbytes = null;
                using (var memorystream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memorystream);
                    file.Dispose();
                    imageasbytes = memorystream.ToArray();
                }

                if (imageasbytes.Length > 0)
                {
                    imageasbytes = _IImageResize.ResizeImage(imageasbytes, 300, 400, 100);
                    Retailer_Form_Image.Add(new MultipleImage_Class
                    {
                        Image = imageasbytes
                    });
                    _Form_Image = Retailer_Form_Image;
                }
            }
            Wait.Dispose();
        }

        private async Task ExecuteCameraAdharPicCommand()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            InitMedia();
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());

            if (file == null)
            {
                return;
            }
            else
            {

                byte[] imageasbytes = null;
                using (var memorystream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memorystream);
                    file.Dispose();
                    imageasbytes = memorystream.ToArray();
                }

                if (imageasbytes.Length > 0)
                {
                    imageasbytes = _IImageResize.ResizeImage(imageasbytes, 300, 400, 100);
                    _GSTN_Adhar_No_Image = imageasbytes;
                }
            }
            Wait.Dispose();
        }

        private async Task ExecuteCameraMultiplePicCommand()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            InitMedia();
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());
            if (file == null)
            {
                return;
            }
            else
            {

                byte[] imageasbytes = null;
                using (var memorystream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memorystream);
                    file.Dispose();
                    imageasbytes = memorystream.ToArray();
                }

                if (imageasbytes.Length > 0)
                {
                    imageasbytes = _IImageResize.ResizeImage(imageasbytes, 300, 400, 100);
                    Retailer_Secondar_Image.Add(new MultipleImage_Class
                    {
                        Image = imageasbytes

                    });
                    _Retailer_Secondar_Image = Retailer_Secondar_Image;
                }
            }
            Wait.Dispose();
        }

        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
            // throw new NotImplementedException();
        }



        private async Task ExecuteCameraSinglePicCommandAsync()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            InitMedia();
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());

            if (file == null)
            {
                return;
            }
            else
            {

                byte[] imageasbytes = null;
                using (var memorystream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memorystream);
                    file.Dispose();
                    imageasbytes = memorystream.ToArray();
                }

                if (imageasbytes.Length > 0)
                {
                    imageasbytes = _IImageResize.ResizeImage(imageasbytes, 300, 400, 100);
                    _Retailer_Image = imageasbytes;
                }
            }
            Wait.Dispose();
        }

        [Display(Name = "Retailer Name")]
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "Maximum 30 char required")]
        public string _Retailer_Name
        {
            get { return RetailerMaster_Class_Data.Retailer_Name; }
            set
            {
                RetailerMaster_Class_Data.Retailer_Name = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Retailer Single Image")]
        [Required]
        public byte[] _Retailer_Image
        {
            get { return RetailerMaster_Class_Data.Retailer_Imagebyte; }
            set
            {
                RetailerMaster_Class_Data.Retailer_Imagebyte = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Retailer Multiple Image")]
        [Required]
        public ObservableCollection<MultipleImage_Class> _Retailer_Secondar_Image
        {
            get { return Retailer_Secondar_Image; }
            set
            {
                Retailer_Secondar_Image = value;
                NotifyPropertyChanged();


            }
        }
        [Display(Name = "Retailer Email Id ")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Incorrect Email Id.")]
        public string _Email_Id
        {
            get { return RetailerMaster_Class_Data.Email_Id; }
            set
            {
                RetailerMaster_Class_Data.Email_Id = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }


        public ObservableCollection<MultipleContect_Class> _Secondary_Mobile_No
        {
            get { return Secondary_Mobile_No; }
            set
            {
                Secondary_Mobile_No = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }

        public MultipleContect_Class SecondaryMobileNoSelectedItem
        {
            get { return MultipleContect_Class_Data; }
            set
            {
                MultipleContect_Class_Data = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }

        [Display(Name = "Retailer Primary Mobile No.")]
        [Required]
        [StringLength(10, ErrorMessage = "Only 10 Number Insert")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Incorrect Mobile No.")]
        public string _Primary_Mobile_No
        {
            get { return RetailerMaster_Class_Data.Mobile_No; }
            set
            {
                RetailerMaster_Class_Data.Mobile_No = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Retailer GSTN Or Aadhar No.")]
        [Required]
        [StringLength(maximumLength: 16, ErrorMessage = "Maximum 16 char required")]
        public string _GSTN_Adhar_No
        {
            get { return RetailerMaster_Class_Data.GSTN_Adhar_No; }
            set
            {
                RetailerMaster_Class_Data.GSTN_Adhar_No = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Retailer GSTN Or Aadhar Image")]
        [Required]
        public byte[] _GSTN_Adhar_No_Image
        {
            get { return RetailerMaster_Class_Data.Adhar_ByteImage; }
            set
            {
                RetailerMaster_Class_Data.Adhar_ByteImage = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Retailer Singnature Form Image ")]
        [Required]
        public ObservableCollection<MultipleImage_Class> _Form_Image
        {
            get { return Retailer_Form_Image; }
            set
            {
                Retailer_Form_Image = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        IList<StateMaster> StateMasterList;
        public IList<StateMaster> _State_Master_List
        {
            get { return StateMasterList; }
            set
            {
                StateMasterList = value;
                NotifyPropertyChanged();
                ValidateProperty(StateMasterList);
            }
        }
        IList<CityMaster> CityMasterList;
        public IList<CityMaster> _City_Master_List
        {
            get { return CityMasterList; }
            set
            {
                CityMasterList = value;
                NotifyPropertyChanged();
                ValidateProperty(CityMasterList);
            }
        }
        int Index;
        public int StateIndex
        {
            get { return Index; }
            set
            {
                Index = value;
                ChangeIndex(Index);
            }
        }

        private void ChangeIndex(int index)
        {
            _City_Master_List = _State_Master_List[index].CityClassList;
        }
        [Display(Name = "Select Retailer State")]
        [Required]
        public int _Retailer_State_Id
        {
            get { return RetailerMaster_Class_Data.State_Id; }
            set
            {

                RetailerMaster_Class_Data.State_Id = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }

        [Display(Name = "Select Retailer City")]
        [Required]
        public int _Retailer_City_Id
        {
            get { return RetailerMaster_Class_Data.City_Id; }
            set
            {
                RetailerMaster_Class_Data.City_Id = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }

        [Display(Name = "Retailer Address")]
        [Required]
        [StringLength(100)]
        public string _Address
        {
            get { return RetailerMaster_Class_Data._Address; }
            set
            {
                RetailerMaster_Class_Data._Address = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }






    }
}
