using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using RealEstateManager.Utils;

namespace RealEstateManager.Models.Estate
{
    public abstract class EstateBaseModel
    {
        public abstract List<HttpPostedFileBase> Images { get; set; }

        public EstateImageModel[] GetSafeImages(HttpServerUtilityBase server)
        {
            return Images
                .Where(x => x != null &&
                    MimeMapping.GetMimeMapping(x.FileName).StartsWith("image/") &&
                    !string.IsNullOrWhiteSpace(Path.GetExtension(x.FileName)))
                .Select(x => new EstateImageModel
                {
                    File = x,
                    SaveLocation = Path.Combine(
                        server.MapPath(ConfigReader.ImageUploadDirectory),
                        $"{Guid.NewGuid()}{Path.GetExtension(x.FileName)}")
                })
                .ToArray();
        }
    }
}
