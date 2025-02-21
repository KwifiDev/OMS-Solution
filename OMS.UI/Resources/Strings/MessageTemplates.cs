namespace OMS.UI.Resources.Strings
{
    public static class MessageTemplates
    {
        // Success Messages
        public static string SuccessMessage => "تم تنفيذ الاجراء بنجاح";
        public static string DeletionSuccessMessage => "تمت عملية الحذف بنجاح";
        public static string AdditionSuccessMessage => "تمت عملية الإضافة بنجاح";
        public static string UpdateSuccessMessage => "تمت عملية التعديل بنجاح";
        public static string SaveSuccessMessage => "تم حفظ البيانات بنجاح";
        public static string LoadSuccessMessage => "تم تحميل البيانات بنجاح";
        public static string VerificationSuccessMessage => "تم التحقق من البيانات بنجاح";

        // Error Messages
        public static string ErrorMessage => "حدث خطأ أثناء تنفيذ هذا الاجراء";
        public static string DeletionErrorMessage => "حدث خطأ أثناء عملية الحذف";
        public static string AdditionErrorMessage => "حدث خطأ أثناء عملية الإضافة";
        public static string UpdateErrorMessage => "حدث خطأ أثناء عملية التعديل";
        public static string SaveErrorMessage => "حدث خطأ أثناء عملية الحفظ";
        public static string LoadErrorMessage => "حدث خطأ أثناء عملية التحميل";
        public static string VerificationErrorMessage => "حدث خطأ أثناء عملية التحقق";
        public static string SearchErrorMessage => "حدث خطأ أثناء عملية البحث عن النموذج";

        // Confirmation Messages
        public static string DeletionConfirmation => "هل انت متأكد من إجراء هذه العملية؟\nسيتم خسارة جميع البيانات ولن يتم استرجاعها.";
        public static string AdditionConfirmation => "هل أنت متأكد من رغبتك في إضافة هذه البيانات؟";
        public static string UpdateConfirmation => "هل أنت متأكد من رغبتك في تعديل هذه البيانات؟";
        public static string SaveConfirmation => "هل أنت متأكد من رغبتك في حفظ هذه البيانات؟";
        public static string LoadConfirmation => "هل أنت متأكد من رغبتك في تحميل هذه البيانات؟";

        // Warning Messages
        public static string WarningMessage => "تحذير: هذا الإجراء قد يسبب فقدان البيانات.";

        // Validation Messages
        public static string FieldRequiredMessage => "هذا الحقل مطلوب.";
        public static string InvalidEmailFormatMessage => "صيغة البريد الإلكتروني غير صحيحة.";
        public static string FieldLengthExceededMessage => "تجاوز عدد الأحرف المسموح بها.";
        public static string InvalidNumberMessage => "الرجاء إدخال رقم صحيح.";
        public static string PasswordMismatchMessage => "كلمتا المرور غير متطابقتين.";
        public static string InvalidDateMessage => "التاريخ المدخل غير صالح.";
        public static string InvalidRangeMessage => "القيمة المدخلة خارج النطاق المسموح.";
        public static string ValidationErrorMessage(string? fieldName) => $"خطأ في عمليات الادخال:\n{fieldName}";
    }
}
