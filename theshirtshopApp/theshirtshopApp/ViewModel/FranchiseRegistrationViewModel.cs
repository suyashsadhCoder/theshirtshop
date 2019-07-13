namespace theshirtshopApp.ViewModel
{
    using Acr.UserDialogs;
    //using Android.Graphics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Plugin.Geolocator;
    using Plugin.Media;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
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


    public class FranchiseRegistrationViewModel : ModelBase
    {

        private IImageResize _IImageResize;
        private FranchiseMasters FranchiseMasters_Classs;
        public MultipleContect_Class MultipleContect_Class_Data;
        public ObservableCollection<MultipleImage_Class> Franchise_Secondar_Image { get; set; }
        public ObservableCollection<MultipleContect_Class> Secondary_Mobile_No { get; set; }
        private IAllDataServices _IAllDataServices;
        IList<StateMaster> StateMasterList;
        IList<CityMaster> CityMasterList;
        public int Index;

        private Command _cameraCommand;
        private INavigation _INavigation { get; set; }

        // private int PageId { get; set; } 
        public FranchiseRegistrationViewModel(INavigation navigation)
        {
            _INavigation = navigation;
            //  PageId = Id;
            SaveCommand = new Command(async () => await SaveDataAsync());
            FranchiseMasters_Classs = new FranchiseMasters();
            MultipleContect_Class_Data = new MultipleContect_Class();
            Franchise_Secondar_Image = new ObservableCollection<MultipleImage_Class>();

            Secondary_Mobile_No = new ObservableCollection<MultipleContect_Class>() {
                new MultipleContect_Class {
                    Contect_No="",
                    ErrorMessageStatus=false
                }
            };
            _IImageResize = new ImageResize();

            _IAllDataServices = new AllDataServices();

            SelectFranchiseAdharPicCommand = new Command(async () => await SelectFranchiseAdharPicAsync());
            SelectFranchiseSinglePicCommand = new Command(async () => await SelectFranchiseSinglePicAsync());
            SelectFranchisePanPicCommand = new Command(async () => await SelectFranchisePanPicAsync());
            SelectFranchiseMultiplePicCommand = new Command(async () => await SelectFranchiseMultiplePicAsync());

            SelectFranchiseAddressPicCommand = new Command(async () => await SelectFranchiseAddressPicAsync());
            GetStateAsync();
            GetLatitudeLogitudeAsync();

        }

        private  async Task<bool> GetLatitudeLogitudeAsync()
        {
            var test = CrossGeolocator.Current;
            if (test.IsGeolocationEnabled == false || test.IsGeolocationAvailable==false)
            {
                await App.Current.MainPage.DisplayAlert("Error!", "GPS is unavailable or not enabled!", "Ok");
                return false;

            }
            else
            {
                var Wait = UserDialogs.Instance.Loading("GPS Tracking", null, null, true, MaskType.Black);
                Wait.Show();
                test.DesiredAccuracy = 50;
                var position = await test.GetPositionAsync();
                FranchiseMasters_Classs.Franchise_Latitude = Convert.ToDecimal(position.Latitude);
                FranchiseMasters_Classs.Franchise_Longitude = Convert.ToDecimal(position.Longitude);
                Wait.Hide();

                return true;


            }
        }

        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
            // throw new NotImplementedException();
        }
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }
        private async Task SaveDataAsync()
        {
            try
            {
                if (HasErrors)
                {
                    ScrollToControlProperty(GetFirstInvalidPropertyName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(FranchiseMasters_Classs.Franchise_Address) && !string.IsNullOrEmpty(FranchiseMasters_Classs.FranchiseAadharNo) && !string.IsNullOrEmpty(FranchiseMasters_Classs.Franchise_Address) && !string.IsNullOrEmpty(FranchiseMasters_Classs.Franchise_BankAccountNo) && !string.IsNullOrEmpty(FranchiseMasters_Classs.Franchise_BankIFSCCode) && !string.IsNullOrEmpty(FranchiseMasters_Classs.Franchise_BankName) && !string.IsNullOrEmpty(FranchiseMasters_Classs.Franchise_EmailId) && !string.IsNullOrEmpty(FranchiseMasters_Classs.Franchise_Name) && !string.IsNullOrEmpty(FranchiseMasters_Classs.Franchise_OwnerName) && !string.IsNullOrEmpty(FranchiseMasters_Classs.Franchise_PanNo) && !string.IsNullOrEmpty(FranchiseMasters_Classs.Franchise_PermanentAddress) && !string.IsNullOrEmpty(FranchiseMasters_Classs.Franchise_PrimaryMobileNo) && FranchiseMasters_Classs.Franchise_StateId != 0 && FranchiseMasters_Classs.Franchise_CityId != 0 && _Franchise_Single_Image != null && _Franchise_Pan_Image != null && _Franchise_Adhar_Image != null && _Franchise_Address_Image != null && !string.IsNullOrEmpty(_Franchise_Pincode))
                    {

                        if (await GetLatitudeLogitudeAsync())
                    {
                            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
                            Wait.Show();
                            FranchiseMasters_Classs._MultipleImage_Data = _Franchise_Multiple_Image;
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
                                FranchiseMasters_Classs.Franchise_AlternateMobileNo = AppendContectNo;
                                string asdf = JsonConvert.SerializeObject(FranchiseMasters_Classs);
                                JObject result = await _IAllDataServices.FranchiseRegistration(FranchiseMasters_Classs);
                                if (result != null)
                                {
                                    string type = result["Type"].ToString();
                                    if (type == "1")
                                    {
                                        await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");


                                        var ChackPriousPage = _INavigation.NavigationStack.Where(x => x.Title == "Franchise Registration").FirstOrDefault();

                                        if (ChackPriousPage != null)
                                        {

                                            _INavigation.RemovePage(ChackPriousPage);

                                        }
                                        await _INavigation.PushAsync(new FranchiseRegistrationPage());

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
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(_Franchise_Name))
                        {
                            _Franchise_Name = "";
                        }
                        //else if (string.IsNullOrEmpty(_Franchise_GST_No))
                        //  {
                        //      _Franchise_GST_No = "";
                        // }
                        else if (string.IsNullOrEmpty(_Franchise_Owner_Name))
                        {
                            _Franchise_Owner_Name = "";
                        }
                        else if (string.IsNullOrEmpty(_Franchise_Email_Id))
                        {
                            _Franchise_Email_Id = "";
                        }
                        else if (string.IsNullOrEmpty(_Franchise_Mobile_No))
                        {
                            _Franchise_Mobile_No = "";
                        }
                        else if (string.IsNullOrEmpty(_Franchise_Address))
                        {
                            _Franchise_Address = "";
                        }
                        else if (string.IsNullOrEmpty(_Franchise_Pincode))
                        {
                            _Franchise_Pincode = "";
                        }
                        else if (string.IsNullOrEmpty(_Franchise_Parmanent_Address))
                        {
                            _Franchise_Parmanent_Address = "";
                        }
                        else if (string.IsNullOrEmpty(_Franchise_Pan_No))
                        {
                            _Franchise_Pan_No = "";
                        }
                        else if (string.IsNullOrEmpty(_Franchise_Adhar_No))
                        {
                            _Franchise_Adhar_No = "";
                        }
                        else if (string.IsNullOrEmpty(_Franchise_Bank_Name))
                        {
                            _Franchise_Bank_Name = "";
                        }
                        else if (string.IsNullOrEmpty(_Franchise_Account_No))
                        {
                            _Franchise_Account_No = "";
                        }
                        else if (string.IsNullOrEmpty(_Franchise_IFC_Code))
                        {
                            _Franchise_IFC_Code = "";
                        }
                        else if (_Franchise_State_Id == 0)
                        {
                            _Franchise_State_Id = 0;
                            await App.Current.MainPage.DisplayAlert("Oops !", "Please Select State", "Ok");
                        }
                        else if (_Franchise_City_Id == 0)
                        {
                            _Franchise_City_Id = 0;
                            await App.Current.MainPage.DisplayAlert("Oops !", "Please Select City", "Ok");
                        }
                        else if (_Franchise_Adhar_Image == null)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops !", "Please Select Aadhar Image ", "Ok");
                        }
                        else if (_Franchise_Pan_Image == null)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops !", "Please Select Pan Image ", "Ok");
                        }
                        else if (_Franchise_Address_Image == null)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops !", "Please Select Address Proof Image ", "Ok");
                        }
                        else if (_Franchise_Single_Image == null)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops !", "Please Select Single Image ", "Ok");
                        }
                        else if (_Franchise_Multiple_Image.Count == 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops !", "Please Select Multiple Image ", "Ok");
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error", ee.Message, "Ok");
            }
        }

        private async Task SelectFranchiseAddressPicAsync()
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
                    _Franchise_Address_Image = imageasbytes;
                }
            }
            Wait.Dispose();
        }

        private async Task SelectFranchiseMultiplePicAsync()
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
                    Franchise_Secondar_Image.Add(new MultipleImage_Class
                    {
                        Image = imageasbytes
                    });
                    _Franchise_Multiple_Image = Franchise_Secondar_Image;
                }
            }
            Wait.Dispose();
        }

        private async Task SelectFranchisePanPicAsync()
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
                    _Franchise_Pan_Image = imageasbytes;
                }
            }
            Wait.Dispose();
        }

        private async Task SelectFranchiseSinglePicAsync()
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
                    _Franchise_Single_Image = imageasbytes;
                }
            }
            Wait.Dispose();
        }

        private async Task SelectFranchiseAdharPicAsync()
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
                    _Franchise_Adhar_Image = imageasbytes;
                }
            }
            Wait.Dispose();
        }

        public async Task GetStateAsync()
        {
            JObject result = await _IAllDataServices.GetStateAndCity();
            List<StateMaster> sm = new List<StateMaster>();

            if (result != null)
            {
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
            else
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Please Refresh Page And try Again....", "Ok");
            }

        }

        [Display(Name = "Franchise Name")]
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "Maximum 30 char required")]
        public string _Franchise_Name
        {
            get { return FranchiseMasters_Classs.Franchise_Name; }
            set
            {
                FranchiseMasters_Classs.Franchise_Name = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "Franchise GST No.")]
        [StringLength(maximumLength: 15, ErrorMessage = "Invalid GST No.")]
        public string _Franchise_GST_No
        {
            get { return FranchiseMasters_Classs.Franchise_GSTNo; }
            set
            {
                FranchiseMasters_Classs.Franchise_GSTNo = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "Franchise Owner Name")]
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "Maximum 30 char required")]
        public string _Franchise_Owner_Name
        {
            get { return FranchiseMasters_Classs.Franchise_OwnerName; }
            set
            {
                FranchiseMasters_Classs.Franchise_OwnerName = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Franchise Single Image")]
        [Required]
        public byte[] _Franchise_Single_Image
        {
            get { return FranchiseMasters_Classs.Franchise_PhotoByte; }
            set
            {
                FranchiseMasters_Classs.Franchise_PhotoByte = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "Franchise Multiple Image")]
        [Required]
        public ObservableCollection<MultipleImage_Class> _Franchise_Multiple_Image
        {
            get { return Franchise_Secondar_Image; }
            set
            {
                Franchise_Secondar_Image = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "Franchise Pan No.")]
        [Required]
        [StringLength(50)]
        [RegularExpression("[A-Z]{5}[0-9]{4}[A-Z]{1}", ErrorMessage = "Invalid Pan Number")]
        public string _Franchise_Pan_No
        {
            get { return FranchiseMasters_Classs.Franchise_PanNo; }
            set
            {
                FranchiseMasters_Classs.Franchise_PanNo = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Franchise Pan Image")]
        [Required]
        public byte[] _Franchise_Pan_Image
        {
            get { return FranchiseMasters_Classs.Pan_PhotoByte; }
            set
            {
                FranchiseMasters_Classs.Pan_PhotoByte = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Franchise Aadhar No.")]
        [Required]
        [StringLength(maximumLength: 16, ErrorMessage = "Minimum 12 and maximum 16 number required", MinimumLength = 12)]

        public string _Franchise_Adhar_No
        {
            get { return FranchiseMasters_Classs.FranchiseAadharNo; }
            set
            {
                FranchiseMasters_Classs.FranchiseAadharNo = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Franchise Aadhar Image")]
        [Required]
        public byte[] _Franchise_Adhar_Image
        {
            get { return FranchiseMasters_Classs.Aadhar_PhotoByte; }
            set
            {
                FranchiseMasters_Classs.Aadhar_PhotoByte = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "Franchise Email Id")]
        [Required]
        [StringLength(200)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Incorrect Email Id.")]
        public string _Franchise_Email_Id
        {
            get { return FranchiseMasters_Classs.Franchise_EmailId; }
            set
            {
                FranchiseMasters_Classs.Franchise_EmailId = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "Franchise Mobile No.")]
        [Required]
        [StringLength(10, ErrorMessage = "Only 10 Number Insert")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Incorrect Mobile No.")]
        public string _Franchise_Mobile_No
        {
            get { return FranchiseMasters_Classs.Franchise_PrimaryMobileNo; }
            set
            {
                FranchiseMasters_Classs.Franchise_PrimaryMobileNo = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        //public ObservableCollection<MultipleContect_Class> _Franchise_Alternate_No
        //{
        //    get { return Secondary_Mobile_No; }
        //    set
        //    {
        //        Secondary_Mobile_No = value;
        //        NotifyPropertyChanged();
        //        ValidateProperty(value);
        //        //foreach (var con in value)
        //        //{
        //        //    if (con.Contect_No.Length > 0)
        //        //    {
        //        //        con.ErrorMessageStatus = true;
        //        //    }
        //        //    else
        //        //    {
        //        //        con.ErrorMessageStatus = false;
        //        //    }
        //        //}
        //    }
        //}
        [Display(Name = "Select Franchise State")]
        [Required]
        public int _Franchise_State_Id
        {
            get { return FranchiseMasters_Classs.Franchise_StateId; }
            set
            {
                FranchiseMasters_Classs.Franchise_StateId = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "select Franchise City")]
        [Required]
        public int _Franchise_City_Id
        {
            get { return FranchiseMasters_Classs.Franchise_CityId; }
            set
            {
                FranchiseMasters_Classs.Franchise_CityId = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Franchise Pincode")]
        [Required]
        [StringLength(6)]
        public string _Franchise_Pincode
        {
            get { return FranchiseMasters_Classs.Franchise_Pincode; }
            set
            {
                FranchiseMasters_Classs.Franchise_Pincode = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }

        [Display(Name = "Franchise Address")]
        [Required]
        [StringLength(100)]
        public string _Franchise_Address
        {
            get { return FranchiseMasters_Classs.Franchise_Address; }
            set
            {
                FranchiseMasters_Classs.Franchise_Address = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Franchise Permanent Address")]
        [Required]
        [StringLength(100)]
        public string _Franchise_Parmanent_Address
        {
            get { return FranchiseMasters_Classs.Franchise_PermanentAddress; }
            set
            {
                FranchiseMasters_Classs.Franchise_PermanentAddress = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "Franchise Address Image")]
        [Required]
        public byte[] _Franchise_Address_Image
        {
            get { return FranchiseMasters_Classs.AddressProof_PhotoByte; }
            set
            {
                FranchiseMasters_Classs.AddressProof_PhotoByte = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Franchise Bank Name")]
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Maximum 20 char required")]
        public string _Franchise_Bank_Name
        {
            get { return FranchiseMasters_Classs.Franchise_BankName; }
            set
            {
                FranchiseMasters_Classs.Franchise_BankName = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Franchise Account No.")]
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Maximum 20 number required")]
        public string _Franchise_Account_No
        {
            get { return FranchiseMasters_Classs.Franchise_BankAccountNo; }
            set
            {
                FranchiseMasters_Classs.Franchise_BankAccountNo = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }

        [Display(Name = "Franchise IFSC Code")]
        [Required]
        [StringLength(maximumLength: 15, ErrorMessage = "Maximum 15 char required")]
        [RegularExpression("^[A-Za-z]{4}[a-zA-Z0-9]{7}$", ErrorMessage = "Invalid IFSC Code")]
        public string _Franchise_IFC_Code
        {
            get { return FranchiseMasters_Classs.Franchise_BankIFSCCode; }
            set
            {
                FranchiseMasters_Classs.Franchise_BankIFSCCode = value;
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
                //foreach (var con in value)
                //{
                //    if (con.Contect_No.Length > 0)
                //    {
                //        con.ErrorMessageStatus = true;
                //    }
                //    else
                //    {
                //        con.ErrorMessageStatus = false;
                //    }
                //}

            }
        }
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







        public ICommand SelectFranchiseAdharPicCommand { get; }
        public ICommand SelectFranchiseSinglePicCommand { get; }
        public ICommand SelectFranchisePanPicCommand { get; }
        public ICommand SelectFranchiseMultiplePicCommand { get; }

        public ICommand SelectFranchiseAddressPicCommand { get; }
        public Command SaveCommand { get; }


        private async void InitMedia()
        {
            await CrossMedia.Current.Initialize();
        }

        public bool CanExecuteCameraCommand()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return false;
            }
            return true;
        }

        public ICommand GetPicCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCameraSinglePicCommand(), () => CanExecuteCameraCommand()); }
        }

        public ICommand GetPanPicCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCameraPanPicCommand(), () => CanExecuteCameraCommand()); }
        }
        public ICommand GetAdharPicCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCameraAdharPicCommand(), () => CanExecuteCameraCommand()); }
        }
        public ICommand GetAddressPicCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCameraAddressPicCommand(), () => CanExecuteCameraCommand()); }
        }
        public ICommand GetMultiplePicCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCameraMultiplePicPicCommand(), () => CanExecuteCameraCommand()); }
        }

        public ICommand AddMoreContectCommand { get { return new Command(async () => await AddMoreContect()); } }

        private async Task AddMoreContect()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if (_Secondary_Mobile_No.Count < 5)
            {
                Secondary_Mobile_No.Add(new MultipleContect_Class
                {
                    Contect_No = "",
                    ErrorMessageStatus = false
                });
                _Secondary_Mobile_No = Secondary_Mobile_No;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Maximum five numbers can be added as alternetive contact number.....", "Ok");
            }
            Wait.Dispose();
        }

        private async Task ExecuteCameraMultiplePicPicCommand()
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
                    Franchise_Secondar_Image.Add(new MultipleImage_Class
                    {
                        Image = imageasbytes
                    });
                    _Franchise_Multiple_Image = Franchise_Secondar_Image;
                }
            }
            Wait.Dispose();
        }

        private async Task ExecuteCameraAddressPicCommand()
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

                    _Franchise_Address_Image = imageasbytes.ToArray();
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
                    _Franchise_Adhar_Image = imageasbytes.ToArray();
                }
            }
            Wait.Dispose();
        }

        private async Task ExecuteCameraPanPicCommand()
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
                    _Franchise_Pan_Image = imageasbytes.ToArray();
                }
            }
            Wait.Dispose();
        }
        private async Task ExecuteCameraSinglePicCommand()
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
                    _Franchise_Single_Image = imageasbytes.ToArray();
                }
            }
            Wait.Dispose();
        }

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
                    _Franchise_Multiple_Image.RemoveAt(index);
                });
            }

        }










    }
}
