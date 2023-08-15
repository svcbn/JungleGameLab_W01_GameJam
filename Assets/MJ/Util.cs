using System.ComponentModel;
using System.Reflection;

/// <summary>
/// [MJ] 각종 코드 작성 시, 참조 가능한 유틸 정적 클래스
/// </summary>
public static class Util
{
    public static string ToDescription<T>(this T source)
    {
        FieldInfo fi = source.GetType().GetField(source.ToString());

        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0) return attributes[0].Description;
        else return source.ToString();
    }
}