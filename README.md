# UltraClassConvertingTool
Generic class converter utility
Version 1.00

With this utility you can copy equally named properies from one class to enother.
Use To<T>() or To<T>(StictFlag) extension method.
If you want to throw an exception, if there is no such property in converting class use .To<T>(StictFlags.Stict),
else you can use .To<T>() or .To<T>(StictFlags.Force) - so if there is not existing property in coping class, property will be not initialized.

I think this utility can be helpful in some situations, but when you need it, there is likly architecture problem in your code.

Thank you for usimg this extension.


