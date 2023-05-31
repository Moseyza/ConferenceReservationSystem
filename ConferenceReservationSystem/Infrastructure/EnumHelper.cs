using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ConferenceReservationSystem.Infrastructure
{
    public class EnumHelper
    {
    

        public static List<string> GetDescriptions<T>() 
        {
            var  Attributes = new List<DescriptionAttribute>();
            foreach (var attribute in typeof(T).GetMembers().SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>()))
            {
                Attributes.Add(attribute);
            }

            return Attributes.Select(x => x.Description).ToList();
        }

        public static List<SelectListItem> GetEnumValueAsSelectList<T>() 
        {
            var result = new List<SelectListItem>();
            var values = Enum.GetValues(typeof(T));
            
            foreach (object value in values) 
            {
                result.Add(new SelectListItem() {Value =  ((int)value).ToString() , Text = GetEnumDescription<T>((Enum)value) });
            }
            return result;


        }

        public static string GetEnumDescription<T>(Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {

                return attribute.Description;
            }
            throw new ArgumentException("Item not found.", nameof(enumValue));
        }


    }

    public static class EnumExtentions
    {
       
    }

}
