using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contentics.Models;


record NewsModel(string Title, string ImageSource, string AgentAvatar, string AgentName, DateTime Date)
{
    public static NewsModel[] Latest
        => new[] 
        {
            new NewsModel(
                Title: "Dui fringilla sed massa id facilisi sit accumsan, lacus",
                ImageSource: "background1.png",
                AgentAvatar: "photo1_small.png",
                AgentName: "Agent Smith",
                Date: DateTime.Now.AddMonths(-12)),
            new NewsModel(
                Title: "At id nunc nisl sit odio. In convallis lorem ut faucibus dignissim nascetur",
                ImageSource: "background2.png",
                AgentAvatar: "photo2_small.png",
                AgentName: "Agent Joe",
                Date: DateTime.Now.AddMonths(-2)),
            new NewsModel(
                Title: "Dui fringilla sed massa id facilisi sit accumsan, lacus",
                ImageSource: "background1.png",
                AgentAvatar: "photo1_small.png",
                AgentName: "Agent Smith",
                Date: DateTime.Now.AddMonths(-12)),
            new NewsModel(
                Title: "At id nunc nisl sit odio. In convallis lorem ut faucibus dignissim nascetur",
                ImageSource: "background2.png",
                AgentAvatar: "photo2_small.png",
                AgentName: "Agent Joe",
                Date: DateTime.Now.AddMonths(-2)),
            new NewsModel(
                Title: "Dui fringilla sed massa id facilisi sit accumsan, lacus",
                ImageSource: "background1.png",
                AgentAvatar: "photo1_small.png",
                AgentName: "Agent Smith",
                Date: DateTime.Now.AddMonths(-12)),
            new NewsModel(
                Title: "At id nunc nisl sit odio. In convallis lorem ut faucibus dignissim nascetur",
                ImageSource: "background2.png",
                AgentAvatar: "photo2_small.png",
                AgentName: "Agent Joe",
                Date: DateTime.Now.AddMonths(-2)),
            new NewsModel(
                Title: "Dui fringilla sed massa id facilisi sit accumsan, lacus",
                ImageSource: "background1.png",
                AgentAvatar: "photo1_small.png",
                AgentName: "Agent Smith",
                Date: DateTime.Now.AddMonths(-12)),
            new NewsModel(
                Title: "At id nunc nisl sit odio. In convallis lorem ut faucibus dignissim nascetur",
                ImageSource: "background2.png",
                AgentAvatar: "photo2_small.png",
                AgentName: "Agent Joe",
                Date: DateTime.Now.AddMonths(-2)),

        }; 

};
