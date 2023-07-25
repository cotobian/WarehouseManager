class CreatePalletDetailVm {
  String PO = '';
  String? Item;
  String PalletNo = '';
  int Quantity = 0;

  CreatePalletDetailVm({
    required this.PO,
    required this.Item,
    required this.PalletNo,
    required this.Quantity,
  });

  Map<String, dynamic> toJson() {
    return {'PO': PO, 'Item': Item, 'PalletNo': PalletNo, 'Quantity': Quantity};
  }
}
