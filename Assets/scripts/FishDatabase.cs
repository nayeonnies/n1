using System.Collections.Generic;
using UnityEngine;

public class FishDatabase : MonoBehaviour
{
    public List<Fish> allFish = new List<Fish>();

    void Awake()
    {
        // Make sure the image file names match your files in the Resources folder.
        Sprite img1 = Resources.Load<Sprite>("lunafin_tetra");
        Sprite img2 = Resources.Load<Sprite>("velune");
        Sprite img3 = Resources.Load<Sprite>("opalia");
        Sprite img4 = Resources.Load<Sprite>("aulia_fin");
        Sprite img5 = Resources.Load<Sprite>("blue_hazel_tetra");
        Sprite img6 = Resources.Load<Sprite>("moonglass_guppy");
        Sprite img7 = Resources.Load<Sprite>("mistril_barb");
        Sprite img8 = Resources.Load<Sprite>("coral_whisper");

        // 8 fish data with seasons, types, and prices (5-20 range) filled in
        allFish.Add(new Fish
        {
            fishname = "lunafin tetra",
            season = "summer",
            size = "m",
            type = "elegant",
            color = "파란",
            price = 12,
            temper = "",
            scent = "fr",
            fishImage = img1
        });

        allFish.Add(new Fish
        {
            fishname = "velune",
            season = "autumn",
            size = "l",
            type = "mysterious",
            color = "보라",
            price = 18,
            temper = "b",
            scent = "fl",
            fishImage = img2
        });

        allFish.Add(new Fish
        {
            fishname = "Opalia",
            season = "spring",
            size = "s",
            type = "cute",
            color = "초록",
            price = 7,
            temper = "",
            scent = "ocean",
            fishImage = img3
        });

        allFish.Add(new Fish
        {
            fishname = "Aulia Fin",
            season = "summer",
            size = "m",
            type = "gorgeous",
            color = "주황",
            price = 15,
            temper = "",
            scent = "fr",
            fishImage = img4
        });

        allFish.Add(new Fish
        {
            fishname = "Blue hazel tetra",
            season = "winter",
            size = "m",
            type = "calm",
            color = "파란",
            price = 14,
            temper = "b",
            scent = "ocean",
            fishImage = img5
        });

        allFish.Add(new Fish
        {
            fishname = "moonglass guppy",
            season = "spring",
            size = "s",
            type = "cute",
            color = "노란",
            price = 6,
            temper = "",
            scent = "fr",
            fishImage = img6
        });

        allFish.Add(new Fish
        {
            fishname = "mistril barb",
            season = "winter",
            size = "l",
            type = "gorgeous",
            color = "핑크",
            price = 19,
            temper = "",
            scent = "fl",
            fishImage = img7
        });

        allFish.Add(new Fish
        {
            fishname = "coral whisper",
            season = "summer",
            size = "m",
            type = "elegant",
            color = "코랄",
            price = 10,
            temper = "",
            scent = "fl",
            fishImage = img8
        });
    }

    // Function to retrieve fish info by its index
    public Fish GetFishByIndex(int index)
    {
        if (index < 0 || index >= allFish.Count) return null;
        return allFish[index];
    }
}