using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  class mtdConvertirImagen
  {
    public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
    {
      using (MemoryStream ms = new MemoryStream())
      {
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        return ms.ToArray();
      }
    }

    public static Image ByteArrayToImage(byte[] byteArrayIn) // reduce tamaño de la imagen
    {
      using (MemoryStream ms = new MemoryStream(byteArrayIn))
      {
        Image returnImage = Image.FromStream(ms);
        returnImage = returnImage.GetThumbnailImage(1000, 1000, () => false, IntPtr.Zero); // 134 px = 3.545 cm
        return returnImage;
      }
    }


    public static Image ConvertByteArrayToImage(byte[] byteArrayIn)
    {
      using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArrayIn))
      {
        Image returnImage = Image.FromStream(ms);
        return returnImage;
      }
    }


    public static byte[] ConvertImageToByteArray(System.Drawing.Image imageIn)
    {
      return (byte[])TypeDescriptor.GetConverter(imageIn).ConvertTo(imageIn, typeof(byte[]));
    }

    //public Image ResizeImage(this Image oldImage, int targetSize)
    //{
    //    Size newSize = calculateDimensions(oldImage.Size, targetSize);
    //    return oldImage.GetThumbnailImage(newSize.Width, newSize.Height, () => false, IntPtr.Zero);
    //}


    //static void Main(string[] args)
    //{
    //    ComprimirImagen(@"C:\img\robot.jpg",
    //                    @"C:\img\robot_tratado.jpg",
    //                    70);
    //}

    public static  void ComprimirImagen(string inputFile, string ouputfile, long compression)
    {
      Image image = Image.FromFile(inputFile);
      EncoderParameters eps = new EncoderParameters(1);

      eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, compression);
      string mimetype = GetMimeType(new System.IO.FileInfo(inputFile).Extension);
      ImageCodecInfo ici = GetEncoderInfo(mimetype);

      image.Save(ouputfile, ici, eps);
    }

    static string GetMimeType(string ext)
    {
      //    CodecName FilenameExtension FormatDescription MimeType 
      //    .BMP;*.DIB;*.RLE BMP ==> image/bmp 
      //    .JPG;*.JPEG;*.JPE;*.JFIF JPEG ==> image/jpeg 
      //    *.GIF GIF ==> image/gif 
      //    *.TIF;*.TIFF TIFF ==> image/tiff 
      //    *.PNG PNG ==> image/png 
      switch (ext.ToLower())
      {
        case ".bmp":
        case ".dib":
        case ".rle":
          return "image/bmp";

        case ".jpg":
        case ".jpeg":
        case ".jpe":
        case ".fif":
          return "image/jpeg";

        case "gif":
          return "image/gif";
        case ".tif":
        case ".tiff":
          return "image/tiff";
        case "png":
          return "image/png";
        default:
          return "image/jpeg";
      }
    }

    static  ImageCodecInfo GetEncoderInfo(string mimeType)
    {

      ImageCodecInfo[] encoders;
      encoders = ImageCodecInfo.GetImageEncoders();

      ImageCodecInfo encoder = (from enc in encoders
                                where enc.MimeType == mimeType
                                select enc).First();
      return encoder;

    }

    //public string GetTamañoImagen(byte[] imagen)
    //{
    //  string tamaño = ByteArrayToImage(imagen).Size;
    //}

  }
}
