public interface IDropdownData
{
    string[] ArrayRegion { get; }
    string[] ArrayHeader { get; }
    string[] ArraySector { get; }
    string[] ArrayClass { get; }
}
public class DataManager : IDropdownData
{//��Ӵٿ��� �޴��� ������
    public string[] ArrayRegion { get; } = new string[15] { "����", "�泲", "���", "����", "�뱸", "����", "�λ�", "����", "���", "��õ", "����", "����", "����", "�泲", "���" };
    public string[] ArrayHeader { get; } = new string[5] { "��ȣ��", "��Ǿ�����з���", "��Ǿ����ߺз���", "��Ǿ����Һз���", "ǥ�ػ���з���" };
    public string[] ArraySector { get; } = new string[10] { "����", "�Ҹ�", "����������", "������������", "����", "�ε���", "���С����", "����", "�ü��������Ӵ�", "�����Ƿ�" };
    public string[] ArrayClass { get; } = new string[2]  { "������ ��� ��ŷ", "�ñ����� ��� ��ŷ" };
}
