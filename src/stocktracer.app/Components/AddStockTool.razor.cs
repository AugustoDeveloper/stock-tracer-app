using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace stocktracer.app.Components
{
    public partial class AddStockTool
    {
        [Parameter]
        public EventCallback<string> EventAddSymbol { get; set; }
        
        private List<string> Symbols { get; } = new List<string>();
        private string Symbol { get; set; }

        async private Task AddSymbolAsync()
        {
            await EventAddSymbol.InvokeAsync(Symbol);
            Symbol = string.Empty;
        }
    }
}