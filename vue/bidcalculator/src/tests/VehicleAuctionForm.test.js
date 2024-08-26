import { shallowMount } from '@vue/test-utils';
import VehicleAuctionForm from '../views/HomeView.vue';

describe('VehicleAuctionForm', () => {
  it('renders the form', () => {
    const wrapper = shallowMount(VehicleAuctionForm);
    expect(wrapper.find('form').exists()).toBe(true);
  });

  it('renders the base price input', () => {
    const wrapper = shallowMount(VehicleAuctionForm);
    expect(wrapper.find('input[type="number"]').exists()).toBe(true);
  });

  it('renders the vehicle type select', () => {
    const wrapper = shallowMount(VehicleAuctionForm);
    expect(wrapper.find('select').exists()).toBe(true);
  });

  it('renders the results section', () => {
    const wrapper = shallowMount(VehicleAuctionForm);
    expect(wrapper.find('.results').exists()).toBe(true);
  });

  it('calls the sendRequest method when the base price input changes', () => {
    const wrapper = shallowMount(VehicleAuctionForm);
    const basePriceInput = wrapper.find('input[type="number"]');
    basePriceInput.setValue(100);
    expect(wrapper.vm.sendRequest).toHaveBeenCalledTimes(1);
  });

  it('calls the sendRequest method when the vehicle type select changes', () => {
    const wrapper = shallowMount(VehicleAuctionForm);
    const vehicleTypeSelect = wrapper.find('select');
    vehicleTypeSelect.select('luxury');
    expect(wrapper.vm.sendRequest).toHaveBeenCalledTimes(1);
  });

  it('updates the data when the sendRequest method is called', async () => {
    const wrapper = shallowMount(VehicleAuctionForm);
    const response = {
      basePrice: 200,
      vehiculeType: 'luxury',
      basicBuyerFee: 10,
      sellerSpecialFee: 20,
      associateFee: 30,
      storageFee: 40,
      total: 100
    };
    wrapper.vm.sendRequest = jest.fn(() => Promise.resolve(response));
    await wrapper.vm.sendRequest();
    expect(wrapper.vm.basePrice).toBe(200);
    expect(wrapper.vm.vehiculeType).toBe('luxury');
    expect(wrapper.vm.basicBuyerFee).toBe(10);
    expect(wrapper.vm.sellerSpecialFee).toBe(20);
    expect(wrapper.vm.associateFee).toBe(30);
    expect(wrapper.vm.storageFee).toBe(40);
    expect(wrapper.vm.total).toBe(100);
  });
});
