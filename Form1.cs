using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarPlate_SVM_Training
{
    public partial class Form1 : Form
    {
        List<string> TraingDataPath = new List<string>(); 

        Matrix<float> TrainData;
        Matrix<float> TestData;
        Matrix<int> TrainLabel;
        Matrix<int> TestLabel;

        SVM svm;
        int Counter = 0;
        bool IsDisplayImage = false;

        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            TraingDataPath.Add(@"D:\MachineLearning\HaarRelated\LicPlateImages\Empty");
            TraingDataPath.Add(@"D:\MachineLearning\HaarRelated\LicPlateImages\Noise1");
            TraingDataPath.Add(@"D:\MachineLearning\HaarRelated\LicPlateImages\Noise2");
            TraingDataPath.Add(@"D:\MachineLearning\HaarRelated\LicPlateImages\OneLine1");
            TraingDataPath.Add(@"D:\MachineLearning\HaarRelated\LicPlateImages\TwoLine1");
            LoadTrainData();
        }

        private void LoadTrainData()
        {
            var trainList = new List<float[]>();
            var trainLabel = new List<int>();
            
            List<string> FileNames = new List<string>();
            foreach (var source_path in TraingDataPath)
            {
                FileNames.AddRange(Directory.GetFiles(source_path, "*.jpg", SearchOption.AllDirectories).ToList());
            }
           
            foreach (var fileN in FileNames)
            {
                var imgOrg = CvInvoke.Imread(fileN, LoadImageType.Grayscale);
                var imgResized = new Mat();
                CvInvoke.Resize(imgOrg, imgResized, new Size(60,70), 0,0,Inter.Cubic);  
                //var imgBin = new Mat();
                //CvInvoke.Threshold(imgResized, imgBin, 128, 255, ThresholdType.Binary);
                var arrPath = Path.GetDirectoryName(fileN).Split(Path.DirectorySeparatorChar);
                var label = arrPath[arrPath.Length - 1].Equals("Empty")?0:1;

                byte[] buffer = imgResized.GetData();
                float[] samples = new float[buffer.Length];
                for (int n = 0; n < buffer.Length; n++)
                {
                    samples[n] = Convert.ToSingle(buffer[n]);
                }

                trainList.Add(samples);
                trainLabel.Add(label);
            }
            
            TrainData = new Matrix<float>(To2D(trainList.ToArray()));
            TrainLabel = new Matrix<int>(trainLabel.ToArray());

            MessageBox.Show("Data Loaded.");
        }

        // reference https://stackoverflow.com/questions/26291609/converting-jagged-array-to-2d-array-c-sharp
        private T[,] To2D<T>(T[][] source)
        {
            try
            {
                int FirstDim = source.Length;
                int SecondDim = source.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

                var result = new T[FirstDim, SecondDim];
                for (int i = 0; i < FirstDim; ++i)
                    for (int j = 0; j < SecondDim; ++j)
                        result[i, j] = source[i][j];

                return result;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular.");
            }
        }


        private void btnTrain_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("svm.yml"))
                {
                    svm = new SVM();
                    FileStorage file = new FileStorage("svm.yml", FileStorage.Mode.Read);
                    svm.Read(file.GetNode("opencv_ml_svm"));
                }
                else
                {
                    svm = new SVM();
                    svm.C = 100;
                    svm.Type = SVM.SvmType.CSvc;
                    svm.Gamma = 0.005;
                    svm.SetKernel(SVM.SvmKernelType.Linear);
                    svm.TermCriteria = new MCvTermCriteria(1000, 1e-6);
                    svm.Train(TrainData, Emgu.CV.ML.MlEnum.DataLayoutType.RowSample, TrainLabel);
                    svm.Save("svm.yml");
                }
                MessageBox.Show("SVM is trained.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        List<string> TestDataPath = new List<string>();

        private async void btnTest_Click(object sender, EventArgs e)
        {
            TraingDataPath.Add(@"D:\PGS\Backup\192.168.2.171\empty");
            //TraingDataPath.Add(@"D:\PGS\Backup\192.168.2.171\good\camera2\20180115");
            TraingDataPath.Add(@"D:\MachineLearning\HaarRelated\LicPlateImages\Empty");
            TraingDataPath.Add(@"D:\MachineLearning\HaarRelated\LicPlateImages\Noise1");
            TraingDataPath.Add(@"D:\MachineLearning\HaarRelated\LicPlateImages\Noise2");
            //TraingDataPath.Add(@"D:\MachineLearning\HaarRelated\LicPlateImages\OneLine1");
            //TraingDataPath.Add(@"D:\MachineLearning\HaarRelated\LicPlateImages\TwoLine1");
            TraingDataPath.Add(@"D:\MachineLearning\HaarRelated\LicPlateImages\Testing");
            await LoadTestData();
        }

        public static int[] createRandomArray(int number, int max)
        {
            List<int> ValueNumber = new List<int>();
            for (int i = 0; i < max; i++)
                ValueNumber.Add(i);
            int[] arr = new int[number];
            int count = 0;
            while (count < number)
            {
                Random rd = new Random();
                int index = rd.Next(0, ValueNumber.Count - 1);
                int auto = ValueNumber[index];
                arr[count] = auto;
                ValueNumber.RemoveAt(index);
                count += 1;
            }
            return arr;
        }

        IEnumerable<int> UniqueRandom(int minInclusive, int maxInclusive)
        {
            List<int> candidates = new List<int>();
            for (int i = minInclusive; i <= maxInclusive; i++)
            {
                candidates.Add(i);
            }
            Random rnd = new Random();
            while (candidates.Count > 0)
            {
                int index = rnd.Next(candidates.Count);
                yield return candidates[index];
                candidates.RemoveAt(index);
            }
        }

        private async Task LoadTestData()
        {
            var testList = new List<float[]>();
            var testLabel = new List<int>();

            List<string> FileNames = new List<string>();
            foreach (var source_path in TraingDataPath)
            {
                FileNames.AddRange(Directory.GetFiles(source_path, "*.jpg", SearchOption.AllDirectories).ToList());
            }

            foreach (var fileN in FileNames)
            {
                var imgOrg = CvInvoke.Imread(fileN, LoadImageType.Grayscale);
                var imgResized = new Mat();
                CvInvoke.Resize(imgOrg, imgResized, new Size(60, 70), 0, 0, Inter.Cubic);
                //var imgBin = new Mat();
                //CvInvoke.Threshold(imgResized, imgBin, 128, 255, ThresholdType.Binary);
                var arrPath = Path.GetDirectoryName(fileN).Split(Path.DirectorySeparatorChar);
                var label = arrPath.Contains("empty") ? 0 : 1;

                byte[] buffer = imgResized.GetData();
                float[] samples = new float[buffer.Length];
                for (int n = 0; n < buffer.Length; n++)
                {
                    samples[n] = Convert.ToSingle(buffer[n]);
                }

                testList.Add(samples);
                testLabel.Add(label);

            }

            var shuffledTestList = new List<float[]>();
            var shuffledTestLabel = new List<int>();
            var shuffledFileNames = new List<string>();

            var nums = UniqueRandom(0, testList.Count-1); // createRandomArray(testList.Count, testList.Count);

            try
            {
                foreach (var num in nums)
                {
                    shuffledTestList.Add(testList[num]);
                    shuffledTestLabel.Add(testLabel[num]);
                    shuffledFileNames.Add(FileNames[num]);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            
            TrainData = new Matrix<float>(To2D(shuffledTestList.ToArray()));
            TrainLabel = new Matrix<int>(shuffledTestLabel.ToArray());
            
            TestData = TrainData;
            TestLabel = TrainLabel;

            if (TestData == null)
            {
                return;
            }

            if (svm == null)
            {
                return;
            }

            var truePath = Path.Combine(Application.StartupPath, "OutTrue");
            if(!Directory.Exists(truePath)) Directory.CreateDirectory(truePath);
            var falsePath = Path.Combine(Application.StartupPath, "OutFalse");
            if (!Directory.Exists(falsePath)) Directory.CreateDirectory(falsePath);

            try
            {
                int counter = 0;
                for (int i = 0; i < TestData.Rows; i++)
                {
                    Matrix<float> row = TestData.GetRow(i);
                    
                    float predict = svm.Predict(row);
                    lblTest.Text = "Input Label: " + TestLabel[i, 0].ToString();
                    lblOouputLabel.Text = "Predicted Label: " + predict.ToString();

                    var fileName = Path.GetFileName(shuffledFileNames[i]);

                    if(predict==1)                    
                        File.Copy(shuffledFileNames[i], Path.Combine(truePath, fileName), true);                    
                    else                    
                        File.Copy(shuffledFileNames[i], Path.Combine(falsePath, fileName), true);     
                    
                    if (predict == TestLabel[i, 0])
                    {
                        counter += 1;
                    }

                    if (IsDisplayImage == true)
                    {
                        Image<Gray, byte> imgout = TestData.GetRow(Counter).Mat.Reshape(0, 28).ToImage<Gray, byte>().ThresholdBinary(new Gray(30), new Gray(255));
                        pictureBox1.Image = imgout.Bitmap;
                        await Task.Delay(1000);
                    }
                    //else
                    //{
                    //    await Task.Delay(100);
                    //}
                }

                lblAccuracy.Text = "Accuracy = " + (counter / (float)(TestData.Rows));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
  

        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            var file = e.Data.GetData(DataFormats.FileDrop) as string[];
         
            var fileN = file.FirstOrDefault();
            Debug.Print(fileN);
            if (fileN==null)
            {
                lblTest.Text = $"Input Label: Invalid";
                return;
            }

            var imgOrg = CvInvoke.Imread(fileN, LoadImageType.Grayscale);
            var imgResized = new Mat();
            CvInvoke.Resize(imgOrg, imgResized, new Size(60, 70), 0, 0, Inter.Cubic);
            var arrPath = Path.GetDirectoryName(fileN).Split(Path.DirectorySeparatorChar);


            pictureBox1.BackgroundImage = imgResized.Bitmap;
            
            byte[] buffer = imgResized.GetData();
            float[] samples = new float[buffer.Length];
            for (int n = 0; n < buffer.Length; n++)
            {
                samples[n] = Convert.ToSingle(buffer[n]);
            }

            Matrix<float> row = new Matrix<float>(samples.ToArray());
            if(svm==null)
            {
                if (File.Exists("svm.yml"))
                {
                    svm = new SVM();
                    FileStorage fileStorage = new FileStorage("svm.yml", FileStorage.Mode.Read);
                    svm.Read(fileStorage.GetNode("opencv_ml_svm"));
                }
            }
            float predict = svm.Predict(row.Transpose());            
            lblOouputLabel.Text = "Predicted Label: " + ((predict == 0) ? "EmptyLot" : "HaveCar"); // predict.ToString();


        }


    }
}
