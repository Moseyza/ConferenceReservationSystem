using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Infrastructure
{
    public class MessageHelper
    {
        public static string SaveErrorMessage => "خطا در ذخیره اطلاعات";
        public static string InputDataIsNotValid => "اطلاعات ورودی نامعتبر است";
        public static string ThereIsNotEnoughCapacity => "ظرفیت کافی وجود ندارد";
        public static string SelectedDateIsNotBetweenStartAndEndDate => "تاریخ وارد شده خارج از بازه زمانی همایش می باشد";
        public static string AccompanyingsCountIsNotValid => "تعداد همراهان نامعتبر است!";
        public static string YouRegisteredBefore => "شما قبلا در این همایش ثبت نام کرده اید";

        public static string YourRegistrationSaveDone => "با تشکر، ثبت نام شما با موفقیت انجام شد. لطفا در موعد مقرر در محل همایش حضور به هم رسانید.";


    }
}
