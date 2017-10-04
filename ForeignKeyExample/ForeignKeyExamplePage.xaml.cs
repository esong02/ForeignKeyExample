using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ForeignKeyExample
{
    public partial class ForeignKeyExamplePage : ContentPage
    {
        public ForeignKeyExamplePage()
        {
            InitializeComponent();
			var db = App.DB.enableFKey;
            //App.DB.SaveAssetAsync(new Asset { Name = "Asset 1", Address = "", Comments = "", LocationDescription = "" });
            //App.DB.InsertAssetAsync(new Asset { Name = "Asset 2", Address = "", Comments = "", LocationDescription = "" });


            //RenameAssetList();
            RenameComponentList();

            /*
            App.DB.InsertComponentAsync(new Component { Name = "Component 1-1", ParentAssetId = 1 });
            App.DB.InsertComponentAsync(new Component { Name = "Component 1-2", ParentAssetId = 1 });
            App.DB.InsertComponentAsync(new Component { Name = "Component 1-3", ParentAssetId = 1 });
            App.DB.InsertComponentAsync(new Component { Name = "Component 2-1", ParentAssetId = 2 });
            App.DB.InsertComponentAsync(new Component { Name = "Component 2-2", ParentAssetId = 2 });
            App.DB.InsertComponentAsync(new Component { Name = "Component 3-1", ParentAssetId = 3 });
            */

            var assetList = App.DB.GetAssetsAsync().Result;
            AssetListView.ItemsSource = assetList;

            var componentList = App.DB.GetComponentsAsync().Result;
            ComponentListView.ItemsSource = componentList;

		}

        void RenameComponentList()
        {
            var componentList = App.DB.GetComponentsAsync().Result;
            foreach(Component component in componentList){
                component.Name = "Component " + component.Id;
                App.DB.UpdateComponentAsync(component);
            }

        }

        void RenameAssetList()
        {
            var assetList = App.DB.GetAssetsAsync().Result;

            assetList[2].Name = "Asset 3";
            assetList[3].Name = "Asset 4";

            App.DB.UpdateAssetAsync(assetList[2]);
            App.DB.UpdateAssetAsync(assetList[3]);

        }
    }
}
