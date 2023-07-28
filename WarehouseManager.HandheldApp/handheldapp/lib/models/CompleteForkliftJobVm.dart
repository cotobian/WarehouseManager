class CompleteForkliftJobVm {
  String PalletNo = '';
  String PositionName = '';

  CompleteForkliftJobVm({
    required this.PositionName,
    required this.PalletNo,
  });

  Map<String, dynamic> toJson() {
    return {'PositionName': PositionName, 'PalletNo': PalletNo};
  }
}
