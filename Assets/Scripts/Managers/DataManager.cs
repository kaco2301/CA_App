public interface IDropdownData
{
    string[] ArrayRegion { get; }
    string[] ArrayHeader { get; }
    string[] ArraySector { get; }
    string[] ArrayClass { get; }
}
public class DataManager : IDropdownData
{
    //��Ӵٿ��� �޴��� ������
    public string[] ArrayRegion { get; } = new string[] { "����", "�泲", "���", "����", "�뱸", "����", "�λ�", "����", "���", "��õ", "����", "����", "����", "�泲", "���" };
    public string[] ArrayHeader { get; } = new string[] { "��ȣ��", "��Ǿ�����з���", "��Ǿ����ߺз���", "��Ǿ����Һз���", "ǥ�ػ���з���" };
    public string[] ArraySector { get; } = new string[] { "����", "�Ҹ�", "����������", "������������", "����", "�ε���", "���С����", "����", "�ü��������Ӵ�", "�����Ƿ�" };
    public string[] ArrayClass { get; } = new string[]  { "������ ��� ��ŷ", "�ñ����� ��� ��ŷ" };
}

