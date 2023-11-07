interface Resource {
  logIn: {
    cardTitle: string;
    welcome: string;
    logInFromHere: string;
    serialNumber: string;
    password: string;
    rememberMe: string;
    didYouForgetPassword: string;
    LogInButton: string;
  };
  NavBar: {
    mainPageLink: string;
    notesLink: string;
    notificationsLink: string;
    callUs: string;
  };
  Weeks: Week[];
  DaysOfWeek: string[];
  styles: AnnotationStyle[];
  AdminOptionsBar: Links[];
  UserOptionsBar: Links[];
  ConsumerOptionsBar: Links[];
  AccountantOptionsBar: Links[];
  CustomersTypes: CustomerTranslatableType[];
  AdminServices: AdminService[];
  UserServices: AdminService[];
  Transactions: TranslatableTransactionType[];
  Days: EnglishDaysTranslator[];
}
export default Resource;
